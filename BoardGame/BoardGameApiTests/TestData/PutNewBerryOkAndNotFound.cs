using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PutNewBerryOkAndNotFound : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 1 2"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 4 1"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 2 3"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.NewGuid(), new Berry {BerryCoordinates = "B 1 2"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.Empty, new Berry {BerryCoordinates = "B 1 2"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
