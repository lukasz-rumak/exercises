using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGameApi.Interfaces;
using BoardGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Wall = BoardGameApi.Models.Wall;

namespace BoardGameApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardGameController : ControllerBase
    {
        private readonly IGameHolder _gameHolder;
        private readonly IPlayer _playersHandler;
        private readonly IPresentation _presentation;
        private readonly IValidator _validator;
        private readonly IValidatorWall _validatorWall;
        private readonly IValidatorBerry _validatorBerry;
        private readonly IEventHandler _eventHandler;

        public BoardGameController(IGameHolder gameHolder, IPlayer playersHandler, IPresentation presentation, IValidator validator, IEventHandler eventHandler, IValidatorWall validatorWall, IValidatorBerry validatorBerry) 
        {
            _gameHolder = gameHolder;
            _playersHandler = playersHandler;
            _presentation = presentation;
            _validator = validator;
            _eventHandler = eventHandler;
            _validatorWall = validatorWall;
            _validatorBerry = validatorBerry;
        }

        [HttpPost("gameInit")]
        public ActionResult<GenericResponse> PostGameInit([FromBody] Board boardInit)
        {
            var sessionId = Guid.NewGuid();
            var game = new GameMaster(_presentation, _eventHandler, _validator, _validatorWall, _validatorBerry, _playersHandler);
            _gameHolder.Add(sessionId, game);
            RunInTheGame(sessionId).StartBoardBuilder(new BoardBuilder(game.ObjectFactory.Get<IEventHandler>(), game.ObjectFactory.Get<IValidatorWall>(), game.ObjectFactory.Get<IValidatorBerry>())
                .WithSize(boardInit.WithSize));
            var lastEvent = RunInTheGame(sessionId).GetLastEvent();
            return lastEvent.Type == EventType.GameStarted
                ? ReturnStatusCodeWithResponse(200, sessionId, "The game has started")
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"The game did not start. Please check request"}});
        }

        [HttpPut]
        [Route("newWall/{sessionId}")]
        public ActionResult<GenericResponse> PutNewWall(Guid sessionId, [FromBody] Wall newWall)
        {
            if (IsSessionIdValid(sessionId) && !IsGameComplete(sessionId) && !IsBoardBuilt(sessionId))
            {
                RunInTheGame(sessionId).AddWallToBoard(newWall.WallCoordinates);
                var lastEvent = RunInTheGame(sessionId).GetLastEvent();
                return lastEvent.Type == EventType.WallCreationDone
                    ? ReturnStatusCodeWithResponse(201, sessionId, "Created")
                    : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {lastEvent.Description}});
            }

            return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
        }
        
        [HttpPut]
        [Route("newBerry/{sessionId}")]
        public ActionResult PutNewBerry(Guid sessionId, [FromBody] Berry newBerry)
        {
            if (IsSessionIdValid(sessionId) && !IsGameComplete(sessionId) && !IsBoardBuilt(sessionId))
            {
                RunInTheGame(sessionId).AddBerryToBoard(newBerry.BerryCoordinates);
                var lastEvent = RunInTheGame(sessionId).GetLastEvent();
                return lastEvent.Type == EventType.BerryCreationDone
                    ? ReturnStatusCodeWithResponse(201, sessionId, "Created")
                    : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {lastEvent.Description}});
            }

            return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
        }

        [HttpPost]
        [Route("buildBoard/{sessionId}")]
        public ActionResult<GenericResponse> PostBuildBoard(Guid sessionId)
        {
            if (IsSessionIdValid(sessionId) && !IsGameComplete(sessionId) && !IsBoardBuilt(sessionId))
            {
                RunInTheGame(sessionId).FinaliseBoardBuilder();
                if (IsBoardBuilt(sessionId) && GetLastEventType(sessionId) == EventType.BoardBuilt)
                {
                    return ReturnStatusCodeWithResponse(201, sessionId, "Created");
                }

                ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"Board not built"}});
            }

            return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
        }

        [HttpPost]
        [Route("addPlayer/{sessionId}")]
        public ActionResult<GenericResponse> PostAddPlayer(Guid sessionId, [FromBody] AddPlayer addPlayer)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
            if (IsGameComplete(sessionId))
                return ReturnNotFoundWithResponseTheGameEnded(sessionId);
            if (!IsBoardBuilt(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInInvalidState(sessionId);
            
            RunInTheGame(sessionId).CreatePlayers(new List<string> {addPlayer.PlayerType});
            return GetLastEventType(sessionId) == EventType.PlayerAdded
                ? ReturnStatusCodeWithResponse(201, sessionId, "Created")
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"Player not created"}});
        }

        [HttpPut]
        [Route("movePlayer/{sessionId}")]
        public ActionResult<GenericResponse> PutMovePlayer(Guid sessionId, [FromBody] MovePlayer movePlayer)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
            if (IsGameComplete(sessionId))
                return ReturnNotFoundWithResponseTheGameEnded(sessionId);
            if (!IsBoardBuilt(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInInvalidState(sessionId);

            RunInTheGame(sessionId).MovePlayer(new List<string> {movePlayer.MoveTo}, movePlayer.PlayerId);
            var lastEvent = RunInTheGame(sessionId).GetLastEvent();
            var result = new List<EventType>
                    {EventType.PieceMoved, EventType.OutsideBoundaries, EventType.FieldTaken, EventType.WallOnTheRoute, EventType.BerryEaten}
                .Any(e => e == lastEvent.Type);
            return result
                ? ReturnStatusCodeWithResponse(200, sessionId, lastEvent.Description)
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {lastEvent.Description}});
        }

        [HttpGet]
        [Route("getEvents/{sessionId}")]
        public ActionResult<GenericResponse> GetEvents(Guid sessionId)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);

            var events = RunInTheGame(sessionId).GetAllEvents();
            var eventsToString = BuildGetEventsResponse(events);
            return !string.IsNullOrWhiteSpace(eventsToString)
                ? ReturnStatusCodeWithResponse(200, sessionId, eventsToString)
                : ReturnStatusCodeWithResponse(200, sessionId, "No events to show");
        }

        [HttpGet]
        [Route("getLastEvent/{sessionId}")]
        public ActionResult<GenericResponse> GetLastEvent(Guid sessionId)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);

            var lastEvent = RunInTheGame(sessionId).GetLastEvent();
            return lastEvent != null
                ? !string.IsNullOrWhiteSpace(lastEvent.Description)
                    ? ReturnStatusCodeWithResponse(200, sessionId, $"{lastEvent.Type}; {lastEvent.Description}")
                    : ReturnStatusCodeWithResponse(200, sessionId, $"{lastEvent.Type}")
                : ReturnStatusCodeWithResponse(200, sessionId, "No event to show");
        }

        [HttpGet]
        [Route("seeBoard/{sessionId}")]
        public ActionResult GetSeeBoard(Guid sessionId)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
            if (!IsBoardBuilt(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInInvalidState(sessionId);

            var output = RunInTheGame(sessionId).GenerateOutputApi();
            return GetLastEventType(sessionId) == EventType.GeneratedBoardOutput
                ? ReturnStatusCodeWithResponse(200, sessionId, output)
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"Board output cannot be generated"}});
        }
        
        [HttpPost]
        [Route("endGame/{sessionId}")]
        public ActionResult PostEndGame(Guid sessionId)
        {
            if (!IsSessionIdValid(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
            if (IsGameComplete(sessionId))
                return ReturnNotFoundWithResponseTheGameEnded(sessionId);

            RunInTheGame(sessionId).MarkGameAsComplete();
            return RunInTheGame(sessionId).IsGameComplete()
                ? ReturnStatusCodeWithResponse(200, sessionId, "The game has been marked as complete")
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"The attempt to mark the game as complete has failed"}});
        }
        
        private bool IsSessionIdValid(Guid sessionId)
        {
            return _gameHolder.IsKeyPresent(sessionId);
        }

        private bool IsGameComplete(Guid sessionId)
        {
            return RunInTheGame(sessionId).IsGameComplete();
        }
        
        private bool IsBoardBuilt(Guid sessionId)
        {
            return RunInTheGame(sessionId).IsBoardBuilt();
        }

        private ObjectResult ReturnStatusCodeWithResponse(int statusCode, Guid sessionId, string response)
        {
            return StatusCode(statusCode, new GenericResponse
            {
                SessionId = sessionId,
                Response = response
            });
        }

        private ObjectResult ReturnBadRequestResponse<T>(T errors) where T : class
        {
            return StatusCode(400, new BadRequestResponse<T>
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                TraceId = "not provided",
                Errors = errors
            });
        }
        
        private ObjectResult ReturnNotFoundWithResponseSessionIdIsInvalid(Guid sessionId)
        {
            return StatusCode(404, new GenericResponse
            {
                SessionId = sessionId,
                Response = "The provided sessionId is invalid"
            });
        }
        
        private ObjectResult ReturnNotFoundWithResponseTheGameEnded(Guid sessionId)
        {
            return StatusCode(404, new GenericResponse
            {
                SessionId = sessionId,
                Response = "The provided sessionId exists but the game has ended"
            });
        }
        
        private ObjectResult ReturnNotFoundWithResponseSessionIdIsInInvalidState(Guid sessionId)
        {
            return StatusCode(404, new GenericResponse
            {
                SessionId = sessionId,
                Response = "The provided sessionId exists but is in invalid state"
            });
        }

        private EventType GetLastEventType(Guid sessionId)
        {
            return RunInTheGame(sessionId).GetLastEvent().Type;
        }

        private string BuildGetEventsResponse(IEnumerable<EventLog> events)
        {
            var strBuilder = new StringBuilder();
            var counter = 0;
            foreach (var e in events)
            {
                strBuilder.Append(!string.IsNullOrWhiteSpace(e.Description)
                    ? $"[{counter}] {e.Type}; {e.Description} "
                    : $"[{counter}] {e.Type} ");
                counter += 1;
            }

            return strBuilder.ToString();
        }

        private IGame RunInTheGame(Guid sessionId)
        {
            return _gameHolder.Get(sessionId);
        }
        
        private string GetPlayerPosition(Guid sessionId, int playerId)
        {
            var playerInfo = RunInTheGame(sessionId).GetPlayerInfo(playerId);
            return $"{playerInfo.Position.X} {playerInfo.Position.Y} {playerInfo.Position.Direction}";
        }
    }
}