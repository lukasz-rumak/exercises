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
            var sessionId = await _testHelper.TestGameInitEndpointForOk(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "The game has started");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 1 1 2 2"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 0 3 0 4"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "S 0 1"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 0 3"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 3 4"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 4 3"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), sessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, sessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "The piece moved and the berry has been eaten. PieceId: 0, PieceType: Pawn, from field (0,1)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "The piece moved. PieceId: 0, PieceType: Pawn, new position: (0,2)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "The piece moved and the berry has been eaten. PieceId: 0, PieceType: Pawn, from field (0,3)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.OK, "Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", sessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId, HttpStatusCode.OK,
                "Player 0: 3 point(s) collected; |---b-|0---b|-----|-----|-----");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "endGame", new object(), sessionId, HttpStatusCode.OK,
                "The game has been marked as complete. The game summary: Player 0: 3 point(s) collected; |---b-|0---b|-----|-----|-----");
        }
        
        [Fact]
        public async Task Test_End_To_End_BoardGameApi_Happy_Path_With_Two_Games()
        {
            var firstGameSessionId = await _testHelper.TestGameInitEndpointForOk(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "The game has started");
            var secondGameSessionId = await _testHelper.TestGameInitEndpointForOk(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "The game has started");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 1 1 2 2"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 0 1 0 2"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 0 3 0 4"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 1 0 2 0"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "S 0 1"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "S 0 1"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 0 3"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 0 3"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 3 4"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 3 4"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 4 3"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newBerry",
                new Berry { BerryCoordinates = "B 4 3"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), firstGameSessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), secondGameSessionId,
                HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, firstGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, secondGameSessionId, HttpStatusCode.Created, "Created");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMM"}, firstGameSessionId, HttpStatusCode.OK, "The piece moved and the berry has been eaten. PieceId: 0, PieceType: Pawn, from field (0,3)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMM"}, firstGameSessionId, HttpStatusCode.OK, "Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, secondGameSessionId, HttpStatusCode.OK, "The piece moved and the berry has been eaten. PieceId: 0, PieceType: Pawn, from field (0,1)");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "MMM"}, secondGameSessionId, HttpStatusCode.OK, "Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,1) to (0, 2)");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", firstGameSessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,3) to (0, 4)");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", secondGameSessionId, HttpStatusCode.OK,
                "WallOnTheRoute; Move not possible (wall on the route)! PieceId: 0, PieceType: Pawn, move from (0,1) to (0, 2)");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", firstGameSessionId,
                HttpStatusCode.OK, "Player 0: 3 point(s) collected; |---b-|0---b|-----|-----|-----");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", secondGameSessionId, HttpStatusCode.OK,
                "Player 0: 2 point(s) collected; |---b-|b---b|-----|0----|-----");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "endGame", new object(), firstGameSessionId, HttpStatusCode.OK,
                "The game has been marked as complete. The game summary: Player 0: 3 point(s) collected; |---b-|0---b|-----|-----|-----");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "endGame", new object(), secondGameSessionId, HttpStatusCode.OK,
                "The game has been marked as complete. The game summary: Player 0: 2 point(s) collected; |---b-|b---b|-----|0----|-----");
        }

        [Fact]
        public async Task Test_Board_Not_Built_Cases()
        {
            var sessionId = await _testHelper.TestGameInitEndpointForOk(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "The game has started");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", new AddPlayer
                    {PlayerType = "P"}, sessionId, HttpStatusCode.NotFound, "The provided sessionId exists but is in invalid state");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                    {PlayerId = 0, MoveTo = "MMMMMMMM"}, sessionId, HttpStatusCode.NotFound, "The provided sessionId exists but is in invalid state");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId,
                HttpStatusCode.NotFound, "The provided sessionId exists but is in invalid state");
        }
        
        [Fact]
        public async Task Test_Game_Ended_Cases()
        {
            var sessionId = await _testHelper.TestGameInitEndpointForOk(_client,
                new Board {WithSize = 5},
                HttpStatusCode.OK, "The game has started");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), sessionId,
                HttpStatusCode.Created, "Created");            
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "endGame", new object(), sessionId, HttpStatusCode.OK,
                "The game has been marked as complete. The game summary: |-----|-----|-----|-----|-----");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall",
                new Wall {WallCoordinates = "W 1 1 2 2"}, sessionId, HttpStatusCode.NotFound,
                "The provided sessionId is invalid");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), sessionId,
                HttpStatusCode.NotFound, "The provided sessionId is invalid");
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", new AddPlayer
                {PlayerType = "P"}, sessionId, HttpStatusCode.NotFound, "The provided sessionId exists but the game has ended");
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", new MovePlayer
                {PlayerId = 0, MoveTo = "M"}, sessionId, HttpStatusCode.NotFound, "The provided sessionId exists but the game has ended");
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", sessionId, HttpStatusCode.OK,
                "GeneratedBoardOutput");
            await _testHelper.TestGetEndpoint(_client, "getEvents", sessionId, HttpStatusCode.OK,
                "[0] GameStarted [1] BoardBuilt [2] GeneratedBoardOutput ");
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId, HttpStatusCode.OK,
                "|-----|-----|-----|-----|-----");
        }
    }
}