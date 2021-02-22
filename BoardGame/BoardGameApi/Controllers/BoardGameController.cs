using System.Collections.Generic;
using System.Text;
using BoardGameApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardGameController : ControllerBase
    {
        private readonly IEnumerable<string> _something;

        public BoardGameController()
        {
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
        public string Post([FromBody] string str)
        {
            return str;
        }
        
        [HttpPost("gameInit")]
        public ActionResult<GameInit> PostGameInit([FromBody] GameInit gameInit)
        {
            return null;
        }

        [HttpPut("newWall")]
        public ActionResult<Wall> PutNewWall([FromBody] Wall newWall)
        {
            return null;
        }
        
        [HttpPost("buildBoard")]
        public ActionResult PostBuildBoard([FromBody] bool buildBoard)
        {
            return Ok();
        }
        
        [HttpPost("addPlayer")]
        public ActionResult PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            return Ok();
        }
        
        [HttpPut("movePlayer")]
        public ActionResult PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            return null;
        }

        [HttpGet("seeBoard")]
        public string SeeBoard()
        {
            return null;
        }
    }
}