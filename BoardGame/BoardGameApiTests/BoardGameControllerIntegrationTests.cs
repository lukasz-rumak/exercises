using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoardGameApi.Models;
using BoardGameApiTests.Helpers;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

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
                new Board {WithSize = 5},
                HttpStatusCode.OK, "Game started");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 1 1 2 2"}, sessionId, HttpStatusCode.Created,
                "Created");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 0 3 0 4"}, sessionId, HttpStatusCode.Created,
                "Created");
            await _testHelper.TestPostEndpoint(_client, "buildBoard", new object(), sessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpoint(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpoint(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMMMMMMM"}, sessionId, HttpStatusCode.OK, "Moved to 0 3 North");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", sessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Event: move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId, HttpStatusCode.OK,
                "Player(s): 1|-----|0----|-----|-----|-----");
        }
        
        [Fact]
        public async Task Test_End_To_End_BoardGameApi_Happy_Path_With_Two_Games()
        {
            var firstGameSessionId = await _testHelper.TestGameInitEndpointAndReturnSessionId(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "Game started");
            var secondGameSessionId = await _testHelper.TestGameInitEndpointAndReturnSessionId(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "Game started");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 1 1 2 2"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 0 1 0 2"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 0 3 0 4"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpoint(_client, "newWall",
                new Wall {WallCoordinates = "W 1 0 2 0"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpoint(_client, "buildBoard", new object(), firstGameSessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpoint(_client, "buildBoard", new object(), secondGameSessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpoint(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpoint(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpoint(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMMMMMMM"}, firstGameSessionId, HttpStatusCode.OK, "Moved to 0 3 North");
            await _testHelper.TestPutEndpoint(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMMMMMMM"}, secondGameSessionId, HttpStatusCode.OK, "Moved to 0 1 North");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", firstGameSessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Event: move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", secondGameSessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Event: move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,1) to (0, 2)");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", firstGameSessionId,
                HttpStatusCode.OK, "Player(s): 1|-----|0----|-----|-----|-----");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", secondGameSessionId, HttpStatusCode.OK,
                "Player(s): 1|-----|-----|-----|0----|-----");
        }

        [Fact]
        public async Task Test_Board_Not_Built_Cases()
        {
            var sessionId = await _testHelper.TestGameInitEndpointAndReturnSessionId(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "Game started");
            await _testHelper.TestPostEndpoint(_client, "addPlayer", new AddPlayer
                    {PlayerType = "P"}, sessionId, HttpStatusCode.BadRequest, "The provided sessionId exists but is in invalid state");
            await _testHelper.TestPutEndpoint(_client, "movePlayer", new MovePlayer
                    {PlayerId = 0, MoveTo = "MMMMMMMM"}, sessionId, HttpStatusCode.BadRequest, "The provided sessionId exists but is in invalid state");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId,
                HttpStatusCode.BadRequest, "The provided sessionId exists but is in invalid state");
        }
    }
}