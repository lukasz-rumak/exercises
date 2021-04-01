using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoardGameApi.Models;
using BoardGameApiTests.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace BoardGameApiTests.Helpers
{
    public class TestHelper
    {
        public async Task<Guid> TestGameInitEndpointForOk(HttpClient client, Board requestBody, HttpStatusCode statusCodeExpected, string responseExpected)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertGenericResponseContent(responseContent, responseContent.SessionId, responseExpected);

            return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : Guid.NewGuid();
        }
        
        public async Task TestGameInitEndpointForBadRequest<T1, T2>(HttpClient client, T1 requestBody, HttpStatusCode statusCodeExpected, T2 responseExpected) where T1 : class where T2 : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/boardgame/gameInit", stringContent);
            var responseContent = await GetResponseContent<BadRequestResponse<T2>>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertBadRequestResponseContent(responseContent, responseExpected);
        }

        public async Task TestPostEndpointForOkAndNotFound<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, string responseExpected) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertGenericResponseContent(responseContent, sessionId, responseExpected);
        }
        
        public async Task TestPostEndpointForBadRequest<T1, T2>(HttpClient client, string endpointName, T1 requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, T2 responseExpected) where T1 : class where T2 : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<BadRequestResponse<T2>>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertBadRequestResponseContent(responseContent, responseExpected);
        }
        
        public async Task TestPutEndpointForOkAndNotFound<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, string responseExpected) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<GenericResponse>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertGenericResponseContent(responseContent, sessionId, responseExpected);
        }
        
        public async Task TestPutEndpointForBadRequest<T1, T2>(HttpClient client, string endpointName, T1 requestBody, Guid sessionId,
            HttpStatusCode statusCodeExpected, T2 responseExpected) where T1 : class where T2 : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetResponseContent<BadRequestResponse<T2>>(response);
            AssertStatusCode(response, statusCodeExpected);
            AssertBadRequestResponseContent(responseContent, responseExpected);
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
            AssertGenericResponseContent(responseContent, sessionId, responseExpected);
        }

        private async Task<T> GetResponseContent<T>(HttpResponseMessage responseMessage) where T : class
        {
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
        }

        private void AssertStatusCode(HttpResponseMessage responseMessage, HttpStatusCode statusCodeExpected)
        {
            responseMessage.StatusCode.Should().Be(statusCodeExpected);
        }

        private void AssertGenericResponseContent(GenericResponse responseContent, Guid sessionIdExpected, string responseExpected)
        {
            if (responseContent.SessionId != Guid.Empty)
                responseContent.SessionId.Should().Be(sessionIdExpected);
            responseContent.Response.Should().Be(responseExpected);
        }
        
        private void AssertBadRequestResponseContent<T>(BadRequestResponse<T> responseContent, T responseExpected) where T : class
        {
            responseContent.Errors.Should().BeEquivalentTo(responseExpected);
        }
    }
}