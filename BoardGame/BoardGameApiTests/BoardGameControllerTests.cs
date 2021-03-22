using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoardGameApi.Models;
using BoardGameApiTests.Helpers;
using BoardGameApiTests.TestData;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BoardGameApiTests
{
    public class BoardGameControllerTests : IClassFixture<WebApplicationFactory<BoardGameApi.Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestHelper _testHelper;
        private readonly TestsSetup _testsSetup;
        private readonly Guid _fakeValidGuid;

        public BoardGameControllerTests(WebApplicationFactory<BoardGameApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
            _testHelper = new TestHelper();
            _testsSetup = new TestsSetup();
            _fakeValidGuid = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb");
        }
        
        [Theory]
        [ClassData(typeof(PostGameInitTestData))]
        public async Task Post_GameInit_Should_Return_SessionId_And_Response(Board requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            await _testHelper.TestGameInitEndpointAndReturnSessionId(_client, requestBody, statusCodeShouldBe, responseShouldBe);
        }

        [Theory]
        [ClassData(typeof(PutNewWallTestData))]
        public async Task Put_NewWall_Should_Return_SessionId_And_Response(Wall requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpoint(_client, "newWall", requestBody, requestBody.SessionId, statusCodeShouldBe, responseShouldBe);
        }

        [Theory]
        [ClassData(typeof(PostBuildBoardTestData))]
        public async Task Post_BuildBoard_Should_Return_SessionId_And_Response(Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestPostEndpoint(_client, "buildBoard", requestBody, requestBody.SessionId, statusCodeShouldBe, responseShouldBe);
        }
        
        [Theory]
        [ClassData(typeof(PostAddPlayerTestData))]
        public async Task Post_AddPlayer_Should_Return_SessionId_And_Response(AddPlayer requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestPostEndpoint(_client, "addPlayer", requestBody, requestBody.SessionId, statusCodeShouldBe, responseShouldBe);
        }
        
        [Theory]
        [ClassData(typeof(PutMovePlayerTestData))]
        public async Task Put_MovePlayer_Should_Return_SessionId_And_Response(MovePlayer requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpoint(_client, "movePlayer", requestBody, requestBody.SessionId, statusCodeShouldBe, responseShouldBe);
        }
        
        [Theory]
        [ClassData(typeof(GetEventsTestData))]
        public async Task Get_GetEvents_Should_Return_SessionId_And_Response(Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "getEvents", requestBody, statusCodeShouldBe, responseShouldBe);
        }
        
        [Theory]
        [ClassData(typeof(GetLastEventTestData))]
        public async Task Get_GetLastEvent_Should_Return_SessionId_And_Response(Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", requestBody, statusCodeShouldBe, responseShouldBe);
        }
        
        [Theory]
        [ClassData(typeof(GetSeeBoardTestData))]
        public async Task Get_SeeBoard_Should_Return_Board(Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (requestBody.SessionId == _fakeValidGuid)
                requestBody.SessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "seeBoard", requestBody, statusCodeShouldBe, responseShouldBe);
        }
    }
}