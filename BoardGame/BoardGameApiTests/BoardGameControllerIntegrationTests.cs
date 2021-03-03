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
            var sessionId = await _testHelper.TestGameInitEndpointAndReturnSessionId(_client);
            await _testHelper.TestNewWallEndpoint(_client, sessionId);
            await _testHelper.TestBuildBoardEndpoint(_client, sessionId);
            await _testHelper.TestAddPlayerEndpoint(_client, sessionId);
            await _testHelper.TestMovePlayerEndpoint(_client, sessionId);
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