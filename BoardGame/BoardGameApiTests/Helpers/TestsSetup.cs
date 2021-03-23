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
            var gameInit = new Board {WithSize = 5};

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
            return new SetupSummary {IsOkay = true, Description = "Setup GameInit is OK", SessionId = sessionId};
        }

        public async Task<SetupSummary> BuildBoardSetup(HttpClient client, Guid sessionId)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync($"/boardgame/buildBoard/{sessionId}", stringContent);

            return response.StatusCode == HttpStatusCode.Created
                ? new SetupSummary {IsOkay = true, Description = "Setup BuildBoard is OK", SessionId = sessionId}
                : new SetupSummary {IsOkay = false, Description = "Setup BuildBoard failed"};
        }

        public async Task<SetupSummary> BuildAddPlayerSetup(HttpClient client, Guid sessionId)
        {
            var addPlayer = new AddPlayer {PlayerType = "P"};
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(addPlayer), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync($"/boardgame/addPlayer/{sessionId}", stringContent);

            return response.StatusCode == HttpStatusCode.Created
                ? new SetupSummary {IsOkay = true, Description = "Setup AddPlayer is OK", SessionId = sessionId}
                : new SetupSummary {IsOkay = false, Description = "Setup AddPlayer failed"};
        }
    }
}