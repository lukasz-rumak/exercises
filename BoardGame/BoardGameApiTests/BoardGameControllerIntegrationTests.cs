using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using BoardGameApiTests.Helpers;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BoardGameApiTests
{
    public class BoardGameControllerIntegrationTests : IClassFixture<WebApplicationFactory<BoardGameApi.Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestHelper _testHelper;

        public BoardGameControllerIntegrationTests(WebApplicationFactory<BoardGameApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
            _testHelper = new TestHelper();
        }

        [Fact]
        public async Task Test_End_To_End_BoardGameApi_Happy_Path()
        {
            var sessionId = await _testHelper.TestGameInitEndpointAndReturnSessionId(_client,
                new GameInit {Board = new Board {Wall = new Wall {WallCoordinates = "W 1 1 2 2"}, WithSize = 5}},
                HttpStatusCode.OK, "Game started");
            await _testHelper.TestNewWallEndpoint(_client,
                new Wall {SessionId = sessionId, WallCoordinates = "W 0 3 0 4"}, HttpStatusCode.Created, "Created");
            await _testHelper.TestBuildBoardEndpoint(_client, new Session {SessionId = sessionId},
                HttpStatusCode.Created, "Created");
            await _testHelper.TestAddPlayerEndpoint(_client, new AddPlayer
                {SessionId = sessionId, PlayerType = "P"}, HttpStatusCode.Created, "Created");
            await _testHelper.TestMovePlayerEndpoint(_client, new MovePlayer
                {SessionId = sessionId, PlayerId = 0, Move = "MMMMMMMM"}, HttpStatusCode.OK, "Moved to 0 3 North");
            await _testHelper.TestGetLastEventEndpoint(_client, new Session {SessionId = sessionId}, HttpStatusCode.OK,
                "Event: move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
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
                RequestUri = new Uri($"{_client.BaseAddress}boardgame/seeBoard"),
                Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"),
            };
            var response = await _client.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("something");
        }
    }
}