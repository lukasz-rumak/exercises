using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace BoardGameApiTests.Helpers
{
    public class TestHelper
    {
        public async Task<Guid> TestGameInitEndpointAndReturnSessionId(HttpClient client)
        {
            var gameInit = new GameInit
            {
                Board = new Board
                {
                    Wall = new Wall { WallCoordinates = "W 1 1 2 2" },
                    WithSize = 5
                }
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(gameInit), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = JsonConvert.DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            responseContent.Response.Should().Be("Game started");

            var sessionId = new Guid();
            return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : sessionId;
        }

        public async Task TestNewWallEndpoint(HttpClient client, Guid sessionId)
        {
            var wall = new Wall
            {
                SessionId = sessionId,
                WallCoordinates = "W 0 3 0 4"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(wall), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/boardgame/newWall", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseContent = JsonConvert.DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            responseContent.SessionId.Should().Be(sessionId);
            responseContent.Response.Should().Be("Created");
        }
        
        public async Task TestBuildBoardEndpoint(HttpClient client, Guid sessionId)
        {
            var session = new Session
            {
                SessionId = sessionId
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/buildBoard", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseContent = JsonConvert.DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            responseContent.SessionId.Should().Be(sessionId);
            responseContent.Response.Should().Be("Created");
        }
        
        public async Task TestAddPlayerEndpoint(HttpClient client, Guid sessionId)
        {
            var addPlayer = new AddPlayer
            {
                SessionId = sessionId,
                PlayerId = 0, // TODO to raczej powinno mi zwracac PlayerId
                PlayerType = "P",
                StartPosition = "0 0" // TODO to raczej powinno mi zwracac StartPosition
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(addPlayer), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/AddPlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseContent = JsonConvert.DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            responseContent.SessionId.Should().Be(sessionId);
            responseContent.Response.Should().Be("Created");
        }

        public async Task TestMovePlayerEndpoint(HttpClient client, Guid sessionId)
        {
            var movePlayer = new MovePlayer
            {
                SessionId = sessionId,
                PlayerId = 0,
                Move = "MMMMMMMM"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(movePlayer), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/boardgame/movePlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = JsonConvert.DeserializeObject<GenericResponse>(await response.Content.ReadAsStringAsync());
            responseContent.SessionId.Should().Be(sessionId);
            responseContent.Response.Should().Be("Moved to 0 3 North");
        }
    }
}