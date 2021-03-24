using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<Guid> TestGameInitEndpointAndReturnSessionId(HttpClient client, Board requestBody, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, responseContent.SessionId, responseShouldBe);
            
            return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : Guid.NewGuid();
        }

        public async Task TestPostEndpoint<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeShouldBe, string responseShouldBe) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, sessionId, responseShouldBe);
        }
        
        public async Task TestPutEndpoint<T>(HttpClient client, string endpointName, T requestBody, Guid sessionId,
            HttpStatusCode statusCodeShouldBe, string responseShouldBe) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync($"/boardgame/{endpointName}/{sessionId}", stringContent);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, sessionId, responseShouldBe);
        }

        public async Task TestGetEndpoint(HttpClient client, string endpointName, Guid sessionId, HttpStatusCode statusCodeShouldBe, string responseShouldBe)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{client.BaseAddress}boardgame/{endpointName}/{sessionId}"),
            };
            var response = await client.SendAsync(request);
            var responseContent = await GetGenericResponse(response);
            AssertStatusCode(response, statusCodeShouldBe);
            AssertResponseContent(responseContent, sessionId, responseShouldBe);
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
            if (responseContent.SessionId != Guid.Empty)
                responseContent.SessionId.Should().Be(sessionIdShouldBe);
            responseContent.Response.Should().Be(responseShouldBe);
        }
    }
}