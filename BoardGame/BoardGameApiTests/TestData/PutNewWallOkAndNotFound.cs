using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PutNewWallOkAndNotFound : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 0 0 1 1"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 2 1 3 2"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.NewGuid(), new Wall {WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.Empty, new Wall {WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}