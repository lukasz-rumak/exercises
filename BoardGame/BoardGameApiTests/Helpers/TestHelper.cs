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
        
        public List<ValidationResult> ValidateModel<T>(T model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, context, result, true);

            return result;
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