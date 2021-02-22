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
    public class BoardGameControllerTests : IClassFixture<WebApplicationFactory<BoardGameApi.Startup>>
    {
        private readonly HttpClient _client;

        public BoardGameControllerTests(WebApplicationFactory<BoardGameApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Be_Okay()
        {
            var response = await _client.GetAsync("/boardgame/doGet");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("something 1, something 2, something 3, ");
        }
        
        [Fact]
        public async Task Post_Should_Be_Okay()
        {
            var model = new Dummy
            {
                Something = "something"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/boardgame/doPost", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"Something\":\"something\"}");
        }
        
        [Fact]
        public async Task Post_GameInit_Should_Return_SessionId()
        {
            var model = new GameInit
            {
                Board = new Board
                {
                    Wall = new Wall
                    {
                        WallCoordinates = "dummy string"
                    },
                    WithSize = 5
                }
            };
            var stringContent = new StringContent(await JsonConvert.SerializeObjectAsync(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/boardgame/gameInit", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"SessionId\":\"something\"}");
        }

        [Fact]
        public async Task Put_NewWall_Should_Return_SessionId_And_Status()
        {
            var model = new Wall
            {
                SessionId = new Guid(),
                WallCoordinates = "dummy string"
            };
            var stringContent = new StringContent(await JsonConvert.SerializeObjectAsync(model), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/boardgame/newWall", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"SessionId\":\"something\", \"Status\":\"created\"}");
        }

        [Fact]
        public async Task Post_BuildBoard_Should_Return_SessionId_And_Status()
        {
            var model = new Session
            {
                SessionId = new Guid()
            };
            var stringContent = new StringContent(await JsonConvert.SerializeObjectAsync(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/boardgame/buildBoard", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"SessionId\":\"something\", \"Status\":\"created\"}");
        }
        
        [Fact]
        public async Task Post_AddPlayer_Should_Return_SessionId_And_Status()
        {
            var model = new AddPlayer
            {
                SessionId = new Guid(),
                PlayerId = 0,
                PlayerType = "something",
                StartPosition = "something"
            };
            var stringContent = new StringContent(await JsonConvert.SerializeObjectAsync(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/boardgame/AddPlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"SessionId\":\"something\", \"Status\":\"added\"}");
        }
        
        [Fact]
        public async Task Put_MovePlayer_Should_Return_SessionId_And_Status()
        {
            var model = new MovePlayer
            {
                SessionId = new Guid(),
                PlayerId = 0,
                Move = "something"
            };
            var stringContent = new StringContent(await JsonConvert.SerializeObjectAsync(model), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/boardgame/movePlayer", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("{\"SessionId\":\"something\", \"Status\":\"moved\"}");
        }
        
        [Fact]
        public async Task Get_SeeBoard_Should_Return_Board()
        {
            var response = await _client.GetAsync("/boardgame/seeBoard");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("something");
        }
    }
}