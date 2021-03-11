using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PutNewWallTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = "W 1 2 3 3"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = "W 1 5 3 6"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = "W -1 2 0 2"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = ""}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = " "}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = null}, HttpStatusCode.InternalServerError, ""},
            new object[] 
                {new Wall {SessionId = Guid.Empty, WallCoordinates = null}, HttpStatusCode.InternalServerError, ""}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}