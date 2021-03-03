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
    public class BoardGameControllerTests : IClassFixture<WebApplicationFactory<BoardGameApi.Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestHelper _testHelper;

        public BoardGameControllerTests(WebApplicationFactory<BoardGameApi.Startup> fixture)
        {
            _client = fixture.CreateClient();
            _testHelper = new TestHelper();
        }
        
        [Fact]
        public async Task Post_GameInit_Should_Return_SessionId_And_Response()
        {
            await _testHelper.TestGameInitEndpointAndReturnSessionId(_client);
        }

        [Fact]
        public async Task Put_NewWall_Should_Return_SessionId_And_Response()
        {
            await _testHelper.TestNewWallEndpoint(_client, Guid.Empty);
        }

        [Fact]
        public async Task Post_BuildBoard_Should_Return_SessionId_And_Response()
        {
            await _testHelper.TestBuildBoardEndpoint(_client, Guid.Empty);
        }
        
        [Fact]
        public async Task Post_AddPlayer_Should_Return_SessionId_And_Response()
        {
            await _testHelper.TestAddPlayerEndpoint(_client, Guid.Empty);
        }
        
        [Fact]
        public async Task Put_MovePlayer_Should_Return_SessionId_And_Response()
        {
            await _testHelper.TestMovePlayerEndpoint(_client, Guid.Empty);
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