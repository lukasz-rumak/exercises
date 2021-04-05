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
        [ClassData(typeof(PostGameInitOk))]
        public async Task Post_GameInit_Should_Return_Ok(Board requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            await _testHelper.TestGameInitEndpointForOk(_client, requestBody, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PostGameInitBadRequest))]
        public async Task Post_GameInit_Should_Return_Bad_Request<T>(Board requestBody, HttpStatusCode statusCodeExpected, T responseExpected) where T : class
        {
            await _testHelper.TestGameInitEndpointForBadRequest(_client, requestBody, statusCodeExpected, responseExpected);
        }

        [Theory]
        [ClassData(typeof(PutNewWallOkAndNotFound))]
        public async Task Put_NewWall_Should_Return_Ok_Or_Not_Found(Guid sessionId, Wall requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "newWall", requestBody, sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PutNewWallBadRequest))]
        public async Task Put_NewWall_Should_Return_Bad_Request<T>(Guid sessionId, Wall requestBody, HttpStatusCode statusCodeExpected, T responseExpected) where T : class
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpointForBadRequest(_client, "newWall", requestBody, sessionId, statusCodeExpected, responseExpected);
        }

        [Theory]
        [ClassData(typeof(PostBuildBoardOkAndNotFound))]
        public async Task Post_BuildBoard_Should_Return_SessionId_And_Response(Guid sessionId, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "buildBoard", new object(), sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PostAddPlayerOkAndNotFound))]
        public async Task Post_AddPlayer_Should_Return_Ok_Or_Not_Found(Guid sessionId, AddPlayer requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPostEndpointForOkAndNotFound(_client, "addPlayer", requestBody, sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PostAddPlayerBadRequest))]
        public async Task Post_AddPlayer_Should_Return_Bad_Request<T>(Guid sessionId, AddPlayer requestBody, HttpStatusCode statusCodeExpected, T responseExpected) where T : class
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPostEndpointForBadRequest(_client, "addPlayer", requestBody, sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PutMovePlayerOkAndNotFound))]
        public async Task Put_MovePlayer_Should_Return_Ok_Or_Not_Found(Guid sessionId, MovePlayer requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpointForOkAndNotFound(_client, "movePlayer", requestBody, sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(PutMovePlayerBadRequest))]
        public async Task Put_MovePlayer_Should_Return_Bad_Request<T>(Guid sessionId, MovePlayer requestBody, HttpStatusCode statusCodeExpected, T responseExpected) where T : class
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestPutEndpointForBadRequest(_client, "movePlayer", requestBody, sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(GetEventsOkAndNotFound))]
        public async Task Get_GetEvents_Should_Return_SessionId_And_Response(Guid sessionId, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "getEvents", sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(GetLastEventOkAndNotFound))]
        public async Task Get_GetLastEvent_Should_Return_SessionId_And_Response(Guid sessionId, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "getLastEvent", sessionId, statusCodeExpected, responseExpected);
        }
        
        [Theory]
        [ClassData(typeof(GetSeeBoardOkAndNotFound))]
        public async Task Get_SeeBoard_Should_Return_Board(Guid sessionId, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var gameInitSetup = await _testsSetup.GameInitSetup(_client);
            await _testsSetup.BuildBoardSetup(_client, gameInitSetup.SessionId);
            await _testsSetup.BuildAddPlayerSetup(_client, gameInitSetup.SessionId);
            if (sessionId == _fakeValidGuid)
                sessionId = gameInitSetup.SessionId;
            await _testHelper.TestGetEndpoint(_client, "seeBoard", sessionId, statusCodeExpected, responseExpected);
        }
    }
}