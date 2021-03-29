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
        public async Task<Guid> TestGameInitEndpointAndReturnSessionId(HttpClient client, Board requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertResponseContent(responseContent, responseContent.SessionId, responseExpected);

            return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : Guid.NewGuid();
        }

        public async Task TestPostEndpoint<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, string responseExpected) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertResponseContent(responseContent, sessionId, responseExpected);
        }
        
        public async Task TestPutEndpoint<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, string responseExpected) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertResponseContent(responseContent, sessionId, responseExpected);
        }

        public async Task TestGetEndpoint(HttpClient client, string endpointName, Guid sessionId, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{client.BaseAddress}boardgame/{endpointName}/{sessionId}"),
            };
            var response = await client.SendAsync(request);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertResponseContent(responseContent, sessionId, responseExpected);
        }

        private async Task<T> GetResponseContent<T>(HttpResponseMessage responseMessage) where T : class
        {
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        }

        private async Task<GenericResponse> GetGenericResponse(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<GenericResponse>(await responseMessage.Content.ReadAsStringAsync());
        }

        private void AssertStatusCode(HttpResponseMessage responseMessage, HttpStatusCode statusCodeExpected)
        {
            responseMessage.StatusCode.Should().Be(statusCodeExpected);
        }

        private void AssertResponseContent(GenericResponse responseContent, Guid sessionIdExpected, string responseExpected)
        {
            if (responseContent.SessionId != Guid.Empty)
                responseContent.SessionId.Should().Be(sessionIdExpected);
            responseContent.Response.Should().Be(responseExpected);
        }
        
        private void AssertResponseContentForBadRequest<T>(BadRequestResponse<T> responseContent, T responseExpected) where T : class
        {
            responseContent.Errors.Should().BeEquivalentTo(responseExpected);
        }

        private bool IsStatusCodeOk(HttpStatusCode statusCodeExpected)
        {
            return statusCodeExpected == HttpStatusCode.OK;
        }
        
        private bool IsStatusCodeBadRequest(HttpStatusCode statusCodeExpected)
        {
            return statusCodeExpected == HttpStatusCode.BadRequest;
        }
        
        private bool IsStatusCodeNotFound(HttpStatusCode statusCodeExpected)
        {
            return statusCodeExpected == HttpStatusCode.NotFound;
        }
    }
}