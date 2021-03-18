using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGameApi.Interfaces;
using BoardGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using EventHandler = BoardGame.Managers.EventHandler;
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
        private readonly IEvent _eventHandler;
        private readonly IGameBoard _gameBoard;

        public BoardGameController(IGameHolder gameHolder, IPlayer player, IPresentation presentation, IValidator validator, IEvent eventHandler, IGameBoard gameBoard, IBoardBuilderHolder boardBuilderHolder, IValidatorWall validatorWall) 
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
            var status = false;
            var sessionId = Guid.NewGuid();
            try
            {
                var game = new GameMaster(_presentation, _eventHandler, _validator, _validatorWall, _player);
                _gameHolder.SessionsHolder.Add(sessionId, game);
                var board = new BoardBuilder(game.ObjectFactory.Get<IEvent>(), game.ObjectFactory.Get<IValidatorWall>())
                    .WithSize(boardInit.WithSize).AddWall(boardInit.Wall.WallCoordinates);
                _boardBuilderHolder.BuilderSessionHolder.Add(sessionId, board);
                status = true;
            }
            catch (Exception)
            {
                // ignored
            }

            if (status)
                return ReturnStatusCodeWithResponse(200, sessionId, "Game started");

            return ReturnStatusCodeWithResponse(400, sessionId, "Please provide valid request");
        }

        [HttpPut("newWall")]
        public ActionResult<GenericResponse> PutNewWall([FromBody] Wall newWall)
        {
            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(newWall.SessionId))
            {
                _boardBuilderHolder.BuilderSessionHolder[newWall.SessionId].AddWall(newWall.WallCoordinates);
                var lastEvent = _gameHolder.SessionsHolder[newWall.SessionId].GetLastEvent();
                return lastEvent.Type == EventType.WallCreationDone
                    ? ReturnStatusCodeWithResponse(201, newWall.SessionId, "Created")
                    : ReturnStatusCodeWithResponse(400, newWall.SessionId, lastEvent.Description);
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(newWall.SessionId);
        }

        [HttpPost("buildBoard")]
        public ActionResult<GenericResponse> PostBuildBoard([FromBody] Session session)
        {
            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(session.SessionId))
            {
                var board = _boardBuilderHolder.BuilderSessionHolder[session.SessionId].BuildBoard();
                _gameHolder.SessionsHolder[session.SessionId].RunBoardBuilder(board);
                if (GetLastEventType(session.SessionId) == EventType.BoardBuilt)
                {
                    _boardBuilderHolder.BuilderSessionHolder.Remove(session.SessionId);
                    return ReturnStatusCodeWithResponse(201, session.SessionId, "Created");
                }

                ReturnStatusCodeWithResponse(400, session.SessionId, "Board not built");
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }

        [HttpPost("addPlayer")]
        public ActionResult<GenericResponse> PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            if (!_gameHolder.SessionsHolder.ContainsKey(addPlayer.SessionId))
                return ReturnBadRequestWithResponseSessionIdIsInvalid(addPlayer.SessionId);
            
            _gameHolder.SessionsHolder[addPlayer.SessionId].CreatePlayers(new List<string> {addPlayer.PlayerType});
            return GetLastEventType(addPlayer.SessionId) == EventType.PlayerAdded
                ? ReturnStatusCodeWithResponse(201, addPlayer.SessionId, "Created")
                : ReturnStatusCodeWithResponse(400, addPlayer.SessionId, "Player not created");
        }

        [HttpPut("movePlayer")]
        public ActionResult<GenericResponse> PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            if (!_gameHolder.SessionsHolder.ContainsKey(movePlayer.SessionId))
                return ReturnBadRequestWithResponseSessionIdIsInvalid(movePlayer.SessionId);
            
            _gameHolder.SessionsHolder[movePlayer.SessionId].MovePlayer(new List<string>{movePlayer.Move}, movePlayer.PlayerId);
            var lastEvent = _gameHolder.SessionsHolder[movePlayer.SessionId].GetLastEvent();
            var result = new List<EventType>
                    {EventType.PieceMoved, EventType.OutsideBoundaries, EventType.FieldTaken, EventType.WallOnTheRoute}
                .Any(e => e == lastEvent.Type);
            return result
                ? ReturnStatusCodeWithResponse(200, movePlayer.SessionId,
                    $"Moved to {_gameHolder.SessionsHolder[movePlayer.SessionId].GameStatus.PlayerPosition[movePlayer.PlayerId]}")
                : ReturnStatusCodeWithResponse(400, movePlayer.SessionId, lastEvent.Description);
        }
        
        [HttpGet("getEvents")]
        public ActionResult<GenericResponse> GetEvents(Session session)
        {
            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
            {
                var lastEvent = _gameHolder.SessionsHolder[session.SessionId].GetLastEvent();
                return string.IsNullOrWhiteSpace(lastEvent.Description)
                    ? ReturnStatusCodeWithResponse(500, session.SessionId,
                        $"Something went wrong!")
                    : ReturnStatusCodeWithResponse(200, session.SessionId, lastEvent.Description);
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }
        
        [HttpGet("getLastEvent")]
        public ActionResult<GenericResponse> GetLastEvent(Session session)
        {
            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
            {
                var lastEvent = _gameHolder.SessionsHolder[session.SessionId].GetLastEvent();
                return string.IsNullOrWhiteSpace(lastEvent.Description)
                    ? ReturnStatusCodeWithResponse(500, session.SessionId,
                        $"Something went wrong!")
                    : ReturnStatusCodeWithResponse(200, session.SessionId, lastEvent.Description);
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }

        [HttpGet("seeBoard")]
        public ActionResult GetSeeBoard(Session session)
        {
            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
                return Ok(_gameHolder.SessionsHolder[session.SessionId].GenerateOutputApi());
            
            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }

        private ObjectResult ReturnStatusCodeWithResponse(int statusCode, Guid sessionId, string response)
        {
            return StatusCode(statusCode, new GenericResponse
            {
                SessionId = sessionId,
                Response = response
            });
        }
        
        private ObjectResult ReturnBadRequestWithResponseSessionIdIsInvalid(Guid sessionId)
        {
            return StatusCode(400, new GenericResponse
            {
                SessionId = sessionId,
                Response = "The provided sessionId is invalid"
            });
        }

        private EventType GetLastEventType(Guid sessionId)
        {
            return _gameHolder.SessionsHolder[sessionId].GetLastEvent().Type;
        }
    }
}