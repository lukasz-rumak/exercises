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
        private readonly IEnumerable<string> _something;
        private readonly IGameHolder _gameHolder;
        private readonly IPlayer _player;
        private readonly IPresentation _presentation;
        private readonly IValidator _validator;
        private readonly IEvent _eventHandler;
        private readonly IGameBoard _gameBoard;

        public BoardGameController(IGameHolder gameHolder, IPlayer player, IPresentation presentation, IValidator validator, IEvent eventHandler, IGameBoard gameBoard) 
        {
            _gameHolder = gameHolder;
            _player = player;
            _presentation = presentation;
            _validator = validator;
            _eventHandler = eventHandler;
            _gameBoard = gameBoard;
            _something = new List<string> { "something 1", "something 2", "something 3" };
        }

        [HttpGet("doGet")]
        public string Get()
        {
            var strBuilder = new StringBuilder();
            foreach (var val in _something)
            {
                strBuilder.Append(val);
                strBuilder.Append(", ");
            }
            return strBuilder.ToString();
        }
        
        [HttpPost("doPost")]
        public string Post([FromBody] Dummy dummy)
        {
            return JsonConvert.SerializeObject(dummy);
        }
        
        [HttpPost("gameInit")]
        public ActionResult<GameInit> PostGameInit([FromBody] GameInit gameInit)
        {
            if (string.IsNullOrWhiteSpace(gameInit.ToString()))
                return StatusCode(500);
            
            var sessionId = Guid.NewGuid();
            var newGameBoard = new BoardBuilder(new EventHandler(new ConsoleOutput()), new Validator())
                .WithSize(gameInit.Board.WithSize).AddWall(gameInit.Board.Wall.WallCoordinates).BuildBoard();
            var newGame = new GameMaster(_validator, newGameBoard, _player, _presentation);
            _gameHolder.SessionsHolder.Add(sessionId, newGame);

            return Ok(new GameInit
            {
                SessionId = sessionId,
                Board = new Board
                {
                    Wall = new Wall
                    {
                        WallCoordinates = gameInit.Board.Wall.WallCoordinates
                    },
                    WithSize = gameInit.Board.WithSize
                }
            });
        }

        [HttpPut("newWall")]
        public ActionResult<Wall> PutNewWall([FromBody] Wall newWall)
        {
            if (string.IsNullOrWhiteSpace(newWall.ToString()))
                return StatusCode(500);

            var sth = new BoardBuilder(new EventHandler(new ConsoleOutput()), new Validator()).AddWall(newWall.WallCoordinates);
            // TODO _gameHolder.SessionsHolder[newWall.SessionId]
            

            return Ok(new ResponseStatus
            {
                SessionId = newWall.SessionId,
                Status = "Created"
            });
        }
        
        [HttpPost("buildBoard")]
        public ActionResult PostBuildBoard([FromBody] Session session)
        {
            return StatusCode(500);
        }
        
        [HttpPost("addPlayer")]
        public ActionResult PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            if (string.IsNullOrWhiteSpace(addPlayer.SessionId.ToString()))
                return StatusCode(500);
            
            _gameHolder.SessionsHolder[addPlayer.SessionId].CreatePlayers(new List<string>{addPlayer.PlayerType});
            return Ok(new ResponseStatus
            {
                SessionId = addPlayer.SessionId,
                Status = "Created"
            });
        }
        
        [HttpPut("movePlayer")]
        public ActionResult PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            return StatusCode(500);
        }

        [HttpGet("seeBoard")]
        public ActionResult SeeBoard(Session session)
        {
            // TODO
            return StatusCode(500);
        }
    }
}