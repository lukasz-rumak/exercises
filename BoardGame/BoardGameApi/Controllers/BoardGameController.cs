using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGameApi.Interfaces;
using BoardGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public ActionResult<GameInit> PostGameInit([FromBody] GameInit gameInit)
        {
            if (string.IsNullOrWhiteSpace(gameInit.ToString()))
                return StatusCode(500);

            var status = false;
            var sessionId = Guid.NewGuid();
            try
            {
                var board = new BoardBuilder(new EventHandler(_presentation), _validatorWall)
                    .WithSize(gameInit.Board.WithSize).AddWall(gameInit.Board.Wall.WallCoordinates);
                _boardBuilderHolder.BuilderSessionHolder.Add(sessionId, board);
                status = true;
            }
            catch (Exception e)
            {
                // ignored
            }

            if (status)
            {
                return Ok(new ResponseStatus
                {
                    SessionId = sessionId,
                    Status = "Game started"
                });
            }

            return StatusCode(400, new ResponseStatus
            {
                SessionId = sessionId,
                Status = "The provided parameters are invalid"
            });
        }

        [HttpPut("newWall")]
        public ActionResult<Wall> PutNewWall([FromBody] Wall newWall)
        {
            if (string.IsNullOrWhiteSpace(newWall.ToString()))
                return StatusCode(500);

            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(newWall.SessionId))
            {
                _boardBuilderHolder.BuilderSessionHolder[newWall.SessionId].AddWall(newWall.WallCoordinates);
                return Ok(new ResponseStatus
                {
                    SessionId = newWall.SessionId,
                    Status = "Created"
                });
            }
            return StatusCode(400, new ResponseStatus
            {
                SessionId = newWall.SessionId,
                Status = "The provided sessionId is invalid"
            });
        }
        
        [HttpPost("buildBoard")]
        public ActionResult PostBuildBoard([FromBody] Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);

            if (_boardBuilderHolder.BuilderSessionHolder.ContainsKey(session.SessionId))
            {
                var board = _boardBuilderHolder.BuilderSessionHolder[session.SessionId].BuildBoard();
                var game = new GameMaster(_validator, board, _player, _presentation);
                _gameHolder.SessionsHolder.Add(session.SessionId, game);
                _boardBuilderHolder.BuilderSessionHolder.Remove(session.SessionId);
                return Ok(new ResponseStatus
                {
                    SessionId = session.SessionId,
                    Status = "Created"
                });
            }
            return StatusCode(400, new ResponseStatus
            {
                SessionId = session.SessionId,
                Status = "The provided sessionId is invalid"
            });
        }
        
        [HttpPost("addPlayer")]
        public ActionResult PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            if (string.IsNullOrWhiteSpace(addPlayer.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(addPlayer.SessionId))
            {
                _gameHolder.SessionsHolder[addPlayer.SessionId].CreatePlayers(new List<string>{addPlayer.PlayerType});
                return Ok(new ResponseStatus
                {
                    SessionId = addPlayer.SessionId,
                    Status = "Created"
                });
            }
            return StatusCode(400, new ResponseStatus
            {
                SessionId = addPlayer.SessionId,
                Status = "The provided sessionId is invalid"
            });
        }
        
        [HttpPut("movePlayer")]
        public ActionResult PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            if (string.IsNullOrWhiteSpace(movePlayer.ToString()))
                return StatusCode(500);

            if (_gameHolder.SessionsHolder.ContainsKey(movePlayer.SessionId))
            {
                _gameHolder.SessionsHolder[movePlayer.SessionId].MovePlayer(new List<string>{movePlayer.Move}, movePlayer.PlayerId);
                return Ok(new ResponseStatus
                {
                    SessionId = movePlayer.SessionId,
                    Status = $"Moved to {_gameHolder.SessionsHolder[movePlayer.SessionId].GameStatus.PlayerPosition[movePlayer.PlayerId]}"
                });
            }
            return StatusCode(400, new ResponseStatus
            {
                SessionId = movePlayer.SessionId,
                Status = "The provided sessionId is invalid"
            });
        }

        [HttpGet("seeBoard")]
        public ActionResult SeeBoard(Session session)
        {
            if (string.IsNullOrWhiteSpace(session.ToString()))
                return StatusCode(500);
            
            if (_gameHolder.SessionsHolder.ContainsKey(session.SessionId))
                return Ok(_gameHolder.SessionsHolder[session.SessionId].GenerateOutputApi());
            return StatusCode(400, new ResponseStatus
            {
                SessionId = session.SessionId,
                Status = "The provided sessionId is invalid"
            });
        }
    }
}