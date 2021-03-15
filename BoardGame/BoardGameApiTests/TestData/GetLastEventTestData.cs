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
                {new Session {SessionId = Guid.NewGuid()}, HttpStatusCode.OK, "Event: "},
            new object[]
                {new Session {SessionId = Guid.Empty}, HttpStatusCode.BadRequest, ""},
            new object[]
                {new Session {SessionId = Guid.NewGuid()}, HttpStatusCode.BadRequest, ""}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}