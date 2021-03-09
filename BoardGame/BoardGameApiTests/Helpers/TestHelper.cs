using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace BoardGameApiTests.Helpers
{
    public class TestHelper
    {
        public async Task<Guid> TestGameInitEndpointAndReturnSessionId(HttpClient client, GameInit requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, responseContent.SessionId, responseShouldBe);

            var sessionId = Guid.NewGuid();
            return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : sessionId;
        }

        public async Task TestNewWallEndpoint(HttpClient client, Wall requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/boardgame/newWall", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, requestBody.SessionId, responseShouldBe);
        }
        
        public async Task TestBuildBoardEndpoint(HttpClient client, Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/buildBoard", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, requestBody.SessionId, responseShouldBe);
        }
        
        public async Task TestAddPlayerEndpoint(HttpClient client, AddPlayer requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/AddPlayer", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, requestBody.SessionId, responseShouldBe);
        }

        public async Task TestMovePlayerEndpoint(HttpClient client, MovePlayer requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/boardgame/movePlayer", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, requestBody.SessionId, responseShouldBe);
        }
        
        public async Task TestGetLastEventEndpoint(HttpClient client, Session requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{client.BaseAddress}boardgame/getLastEvent"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"),
            };
            var response = await client.SendAsync(request);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, requestBody.SessionId, responseShouldBe);
        }

        private async Task<GenericResponse> GetGenericResponse(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<GenericResponse>(await responseMessage.Content.ReadAsStringAsync());
        }

        private void AssertStatusCode(HttpResponseMessage responseMessage, HttpStatusCode statusCodeShouldBe)
        {
            responseMessage.StatusCode.Should().Be(statusCodeShouldBe);
        }

        private void AssertResponseContent(GenericResponse responseContent, Guid sessionIdShouldBe, string responseShouldBe)
        {
            responseContent.SessionId.Should().Be(sessionIdShouldBe);
            responseContent.Response.Should().Be(responseShouldBe);
        }
    }
}