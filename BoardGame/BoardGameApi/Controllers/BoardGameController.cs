using System.Collections.Generic;
using System.Text;
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
    }
}