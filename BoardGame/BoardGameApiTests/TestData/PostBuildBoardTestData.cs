using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PostBuildBoardTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new Session {SessionId = Guid.NewGuid()}, HttpStatusCode.Created, "Created"},
            new object[]
                {new Session {SessionId = Guid.Empty}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Session {SessionId = Guid.NewGuid()}, HttpStatusCode.InternalServerError, ""}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}