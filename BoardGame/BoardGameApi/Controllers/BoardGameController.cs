using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BoardGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public string Post([FromBody] Dummy dummy)
        {
            return JsonConvert.SerializeObject(dummy);
        }
        
        [HttpPost("gameInit")]
        public ActionResult<GameInit> PostGameInit([FromBody] GameInit gameInit)
        {
            return StatusCode(500);
        }

        [HttpPut("newWall")]
        public ActionResult<Wall> PutNewWall([FromBody] Wall newWall)
        {
            return StatusCode(500);
        }
        
        [HttpPost("buildBoard")]
        public ActionResult PostBuildBoard([FromBody] Session session)
        {
            return StatusCode(500);
        }
        
        [HttpPost("addPlayer")]
        public ActionResult PostAddPlayer([FromBody] AddPlayer addPlayer)
        {
            return StatusCode(500);
        }
        
        [HttpPut("movePlayer")]
        public ActionResult PutMovePlayer([FromBody] MovePlayer movePlayer)
        {
            return StatusCode(500);
        }

        [HttpGet("seeBoard")]
        public ActionResult SeeBoard()
        {
            return StatusCode(500);
        }
    }
}