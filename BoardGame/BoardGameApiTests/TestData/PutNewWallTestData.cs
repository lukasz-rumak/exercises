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
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 0 0 1 1"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 2 1 3 2"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 1 2 3 3"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input wall position difference should be 0 or 1"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 1 5 3 6"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input wall position difference should be 0 or 1"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W -1 2 0 2"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input wall position should fit into the board size"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "1 2 1 3"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input should start with 'W'"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W X 1 1 3"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input wall position should be integers"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = "W 1 2 1 3 1"}, HttpStatusCode.BadRequest, "Event: The wall(s) were not created! Input wall position should contain four chars divided by whitespace"},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = ""}, HttpStatusCode.BadRequest, null},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = " "}, HttpStatusCode.BadRequest, null},
            new object[]
                {new Wall {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), WallCoordinates = null}, HttpStatusCode.BadRequest, null},
            new object[]
                {new Wall {SessionId = Guid.NewGuid(), WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {new Wall {SessionId = Guid.Empty, WallCoordinates = "W 1 2 1 3"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}