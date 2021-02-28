using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BoardGameApiTests
{
    public class BoardGameControllerIntegrationTests : IClassFixture<WebApplicationFactory<BoardGameApi.Startup>>
    {
        private readonly HttpClient _client;

        public BoardGameControllerIntegrationTests(WebApplicationFactory<BoardGameApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
        
        [Fact]
        public async Task Test_End_To_End_BoardGameApi_Happy_Path()
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
            var response = await _client.PostAsync("/boardgame/gameInit", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("\"status\":\"Game started\"}");

            var sessionId = new Guid();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                sessionId = JsonConvert.DeserializeObject<ResponseStatus>(responseContent).SessionId;    
            }
            
            var wall = new Wall
            {
                SessionId = sessionId,
                WallCoordinates = "W 0 3 0 4"
            };
            stringContent = new StringContent(JsonConvert.SerializeObject(wall), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/boardgame/newWall", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"{{\"sessionId\":\"{sessionId}\",\"status\":\"Created\"}}");
            
            var session = new Session
            {
                SessionId = sessionId
            };
            stringContent = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");
            response = await _client.PostAsync("/boardgame/buildBoard", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"{{\"sessionId\":\"{sessionId}\",\"status\":\"Created\"}}");
            
            var addPlayer = new AddPlayer
            {
                SessionId = sessionId,
                PlayerId = 0, // TODO
                PlayerType = "P",
                StartPosition = "0 0" // TODO
            };
            stringContent = new StringContent(JsonConvert.SerializeObject(addPlayer), Encoding.UTF8, "application/json");
            response = await _client.PostAsync("/boardgame/AddPlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"{{\"sessionId\":\"{sessionId}\",\"status\":\"Created\"}}");
        
            var movePlayer = new MovePlayer
            {
                SessionId = sessionId,
                PlayerId = 0,
                Move = "MMMMMMMM"
            };
            stringContent = new StringContent(JsonConvert.SerializeObject(movePlayer), Encoding.UTF8, "application/json");
            response = await _client.PutAsync("/boardgame/movePlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"{{\"sessionId\":\"{sessionId}\",\"status\":\"Moved to 0 3 North\"}}");
        }
        
        [Fact]
        public async Task Get_SeeBoard_Should_Return_Board()
        {
            var model = new Session
            {
                SessionId = new Guid()
            };
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/boardgame/seeBoard"),
                Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"),
            };
            var response = await _client.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("something");
        }
    }
}