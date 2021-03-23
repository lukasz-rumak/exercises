using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class GetSeeBoardTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), HttpStatusCode.OK, "Player(s): 1|-----|-----|-----|-----|0----"},
            new object[]
                {Guid.Empty, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.NewGuid(), HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}