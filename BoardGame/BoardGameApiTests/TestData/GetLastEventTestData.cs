using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class GetLastEventTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new Session {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb")}, HttpStatusCode.OK, "PlayerAdded"},
            new object[]
                {new Session {SessionId = Guid.Empty}, HttpStatusCode.BadRequest, "The provided sessionId is invalid"},
            new object[]
                {new Session {SessionId = Guid.NewGuid()}, HttpStatusCode.BadRequest, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}