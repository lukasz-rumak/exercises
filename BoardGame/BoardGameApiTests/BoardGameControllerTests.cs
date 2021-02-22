using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            var res = await response.Content.ReadAsStringAsync();
            res.Should().Be("something 1, something 2, something 3, ");
        }
    }
}