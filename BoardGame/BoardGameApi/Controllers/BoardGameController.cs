using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
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
        public ActionResult<GenericResponse> PostGameInit([FromBody] GameInit gameInit)
        {
            if (string.IsNullOrWhiteSpace(gameInit.ToString()))
                return StatusCode(500);

            var status = false;
            var sessionId = Guid.NewGuid();
            try
            {
                var game = new GameMaster(_presentation, _eventHandler, _validator, _validatorWall, _player);
                _gameHolder.SessionsHolder.Add(sessionId, game);
                var board = new BoardBuilder(game.ObjectFactory.Get<IEvent>(), game.ObjectFactory.Get<IValidatorWall>())
                    .WithSize(gameInit.Board.WithSize).AddWall(gameInit.Board.Wall.WallCoordinates);
                _boardBuilderHolder.BuilderSessionHolder.Add(sessionId, board);
                status = true;
            }
            catch (Exception)
            {
                // ignored
            }

            if (status)
                return ReturnStatusCodeWithResponse(200, sessionId, "Game started");

            return ReturnBadRequestWithResponseSessionIdIsInvalid(sessionId);
        }

        [HttpPut("newWall")]
        public ActionResult<GenericResponse> PutNewWall([FromBody] Wall newWall)
        {
            if (string.IsNullOrWhiteSpace(newWall.ToString()))
                return StatusCode(500);

            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(newWall.SessionId))
            {
                _boardBuilderHolder.BuilderSessionHolder[newWall.SessionId].AddWall(newWall.WallCoordinates);
                return ReturnStatusCodeWithResponse(201, newWall.SessionId, "Created");
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(newWall.SessionId);
        }
        
        [HttpPost("buildBoard")]
        public ActionResult<GenericResponse> PostBuildBoard([FromBody] Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);

            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(session.SessionId))
            {
                var board = _boardBuilderHolder.BuilderSessionHolder[session.SessionId].BuildBoard();
                _gameHolder.SessionsHolder[session.SessionId].RunBoardBuilder(board);
                _boardBuilderHolder.BuilderSessionHolder.Remove(session.SessionId);
                return ReturnStatusCodeWithResponse(201, session.SessionId, "Created");
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }
        
        [HttpPost("addPlayer")]
        public ActionResult<GenericResponse> PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            if (string.IsNullOrWhiteSpace(addPlayer.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(addPlayer.SessionId))
            {
                _gameHolder.SessionsHolder[addPlayer.SessionId].CreatePlayers(new List<string> {addPlayer.PlayerType});
                return ReturnStatusCodeWithResponse(201, addPlayer.SessionId, "Created");
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(addPlayer.SessionId);
        }
        
        [HttpPut("movePlayer")]
        public ActionResult<GenericResponse> PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            if (string.IsNullOrWhiteSpace(movePlayer.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(movePlayer.SessionId))
            {
                _gameHolder.SessionsHolder[movePlayer.SessionId].MovePlayer(new List<string>{movePlayer.Move}, movePlayer.PlayerId);
                return ReturnStatusCodeWithResponse(200, movePlayer.SessionId,
                    $"Moved to {_gameHolder.SessionsHolder[movePlayer.SessionId].GameStatus.PlayerPosition[movePlayer.PlayerId]}");
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(movePlayer.SessionId);
        }
        
        [HttpGet("getEvents")]
        public ActionResult<GenericResponse> GetEvents(Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
            {
                var lastEvent = _gameHolder.SessionsHolder[session.SessionId].GetLastEvent();
                return string.IsNullOrWhiteSpace(lastEvent)
                    ? ReturnStatusCodeWithResponse(500, session.SessionId,
                        $"Something went wrong!")
                    : ReturnStatusCodeWithResponse(200, session.SessionId, lastEvent);
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }
        
        [HttpGet("getLastEvent")]
        public ActionResult<GenericResponse> GetLastEvent(Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
            {
                var lastEvent = _gameHolder.SessionsHolder[session.SessionId].GetLastEvent();
                return string.IsNullOrWhiteSpace(lastEvent)
                    ? ReturnStatusCodeWithResponse(500, session.SessionId,
                        $"Something went wrong!")
                    : ReturnStatusCodeWithResponse(200, session.SessionId, lastEvent);
            }

            return ReturnBadRequestWithResponseSessionIdIsInvalid(session.SessionId);
        }

        [HttpGet("seeBoard")]
        public ActionResult GetSeeBoard(Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);
            
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
    }
}