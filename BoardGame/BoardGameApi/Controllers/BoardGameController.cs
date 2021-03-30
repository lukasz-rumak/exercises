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
        private readonly IBoardBuilderHolder _boardBuilderHolder;
        private readonly IPlayer _player;
        private readonly IPresentation _presentation;
        private readonly IValidator _validator;
        private readonly IValidatorWall _validatorWall;
        private readonly IEventHandler _eventHandler;
        private readonly IGameBoard _gameBoard;

        public BoardGameController(IGameHolder gameHolder, IPlayer player, IPresentation presentation, IValidator validator, IEventHandler eventHandler, IGameBoard gameBoard, IBoardBuilderHolder boardBuilderHolder, IValidatorWall validatorWall) 
        {
            _gameHolder = gameHolder;
            _player = player;
            _presentation = presentation;
            _validator = validator;
            _eventHandler = eventHandler;
            _gameBoard = gameBoard;
            _boardBuilderHolder = boardBuilderHolder;
            _validatorWall = validatorWall;
        }

        [HttpPost("gameInit")]
        public ActionResult<GenericResponse> PostGameInit([FromBody] Board boardInit)
        {
            var sessionId = Guid.NewGuid();
            var game = new GameMaster(_presentation, _eventHandler, _validator, _validatorWall, _player);
            _gameHolder.Add(sessionId, game);
            var board = new BoardBuilder(game.ObjectFactory.Get<IEventHandler>(), game.ObjectFactory.Get<IValidatorWall>())
                .WithSize(boardInit.WithSize);
            _boardBuilderHolder.Add(sessionId, board);
            var lastEvent = RunInTheGame(sessionId).GetLastEvent();
            return lastEvent.Type == EventType.GameStarted
                ? ReturnStatusCodeWithResponse(200, sessionId, "The game started")
                : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {"The game did not start. Please check request"}});
        }

        [HttpPut]
        [Route("newWall/{sessionId}")]
        public ActionResult<GenericResponse> PutNewWall(Guid sessionId, [FromBody] Wall newWall)
        {
            if (_boardBuilderHolder.IsKeyPresent(sessionId))
            {
                RunInTheBoardBuilder(sessionId).AddWall(newWall.WallCoordinates);
                var lastEvent = RunInTheGame(sessionId).GetLastEvent();
                return lastEvent.Type == EventType.WallCreationDone
                    ? ReturnStatusCodeWithResponse(201, sessionId, "Created")
                    : ReturnBadRequestResponse(new BadRequestErrors {Errors = new[] {lastEvent.Description}});
            }

            return ReturnNotFoundWithResponseSessionIdIsInvalid(sessionId);
        }

        [HttpPost]
        [Route("buildBoard/{sessionId}")]
        public ActionResult<GenericResponse> PostBuildBoard(Guid sessionId)
        {
            if (_boardBuilderHolder.IsKeyPresent(sessionId))
            {
                var board = RunInTheBoardBuilder(sessionId).BuildBoard();
                RunInTheGame(sessionId).RunBoardBuilder(board);
                if (GetLastEventType(sessionId) == EventType.BoardBuilt)
                {
                    _boardBuilderHolder.Remove(sessionId);
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
            if (!IsBoardBuilt(sessionId))
                return ReturnNotFoundWithResponseSessionIdIsInInvalidState(sessionId);

            RunInTheGame(sessionId).MovePlayer(new List<string> {movePlayer.MoveTo}, movePlayer.PlayerId);
            var lastEvent = RunInTheGame(sessionId).GetLastEvent();
            var result = new List<EventType>
                    {EventType.PieceMoved, EventType.OutsideBoundaries, EventType.FieldTaken, EventType.WallOnTheRoute}
                .Any(e => e == lastEvent.Type);
            return result
                ? ReturnStatusCodeWithResponse(200, sessionId,
                    $"Moved to {GetPlayerPosition(sessionId, movePlayer.PlayerId)}")
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

        private bool IsSessionIdValid(Guid sessionId)
        {
            return _gameHolder.IsKeyPresent(sessionId);
        }
        
        private bool IsBoardBuilt(Guid sessionId)
        {
            return RunInTheGame(sessionId).GetAllEvents().Any(e => e.Type == EventType.BoardBuilt);
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

        private IBoardBuilder RunInTheBoardBuilder(Guid sessionId)
        {
            return _boardBuilderHolder.Get(sessionId);
        }

        private string GetPlayerPosition(Guid sessionId, int playerId)
        {
            var playerInfo = RunInTheGame(sessionId).GetPlayerInfo(playerId);
            return $"{playerInfo.Position.X} {playerInfo.Position.Y} {playerInfo.Position.Direction}";
        }
    }
}