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
        public async Task<Guid> TestGameInitEndpointAndReturnSessionId<T>(HttpClient client, Board requestBody, HttpStatusCode statusCodeShouldBe, T responseShouldBe) where T : class
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/boardgame/gameInit", stringContent);
            AssertStatusCode(response, statusCodeShouldBe);
            if (IsStatusCodeOk(statusCodeShouldBe))
            {
                var responseContent = await GetResponseContent<GenericResponse>(response);
                if (responseContent.SessionId != Guid.Empty)
                    responseContent.SessionId.Should().Be(responseContent.SessionId);
                if (responseShouldBe.GetType() == typeof(GenericResponse))
                {
                    var neObj = (GenericResponse) responseShouldBe;
                    new GenericResponse
                    {
                        SessionId = responseContent.SessionId,
                        Response = responseShouldBe.
                    }
                }
                    
                    
                responseContent.Should().BeEquivalentTo(responseShouldBe);
                //AssertResponseContent(responseContent, responseContent.SessionId, responseShouldBe);
                return !string.IsNullOrWhiteSpace(responseContent.ToString()) ? responseContent.SessionId : Guid.NewGuid();
            }
            if (IsStatusCodeBadRequest(statusCodeShouldBe))
            {
                var responseContent = await GetResponseContent<BadRequestResponse<BadRequestWithSize>>(response);
                responseContent.Errors.Should().BeEquivalentTo(responseShouldBe);
                //AssertResponseContentForBadRequest(responseContent, responseShouldBe);
            }
                
            return Guid.NewGuid();
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

        private async Task<T> GetResponseContent<T>(HttpResponseMessage responseMessage) where T : class
        {
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
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
        
        private void AssertResponseContentForBadRequest<T>(BadRequestResponse<T> responseContent, T responseShouldBe) where T : class
        {
            responseContent.Errors.Should().BeEquivalentTo(responseShouldBe);
        }

        private bool IsStatusCodeOk(HttpStatusCode statusCodeShouldBe)
        {
            return statusCodeShouldBe == HttpStatusCode.OK;
        }
        
        private bool IsStatusCodeBadRequest(HttpStatusCode statusCodeShouldBe)
        {
            return statusCodeShouldBe == HttpStatusCode.BadRequest;
        }
        
        private bool IsStatusCodeNotFound(HttpStatusCode statusCodeShouldBe)
        {
            return statusCodeShouldBe == HttpStatusCode.NotFound;
        }
    }
}