using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using BoardGameApiTests.Models;
using Newtonsoft.Json;

namespace BoardGameApiTests.Helpers
{
    public class TestsSetup
    {
        public async Task<SetupSummary> GameInitSetup(HttpClient client)
        {
            var gameInit = new GameInit
                {Board = new Board {Wall = new Wall {WallCoordinates = "W 1 1 2 2"}, WithSize = 5}};

            var stringContent =
                new StringContent(JsonConvert.SerializeObject(gameInit), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            if (response.StatusCode != HttpStatusCode.OK)
                return new SetupSummary {IsOkay = false, Description = "Setup GameInit failed"};
            var responseContent = JsonConvert
                .DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            if (responseContent == null)
                return new SetupSummary {IsOkay = false, Description = "Setup GameInit failed"};
            var sessionId = responseContent.SessionId;
            return new SetupSummary {IsOkay = false, Description = "Setup GameInit is OK", SessionId = sessionId};
        }

        public async Task<SetupSummary> BuildBoardSetup(HttpClient client, Guid sessionId)
        {
            var sessionIdObject = new Session {SessionId = sessionId};

            var stringContent = new StringContent(JsonConvert.SerializeObject(sessionIdObject), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("/boardgame/buildBoard", stringContent);

            return response.StatusCode == HttpStatusCode.Created
                ? new SetupSummary {IsOkay = false, Description = "Setup BuildBoard is OK", SessionId = sessionId}
                : new SetupSummary {IsOkay = false, Description = "Setup BuildBoard failed"};
        }
    }
}