using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PostAddPlayerTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "P"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "K"}, HttpStatusCode.Created, "Created"}, 
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "PP"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "X"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "Y"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "XX"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = "!"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = ""}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = " "}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerType = null}, HttpStatusCode.BadRequest, null},
            new object[]
                {new AddPlayer {SessionId = Guid.Empty, PlayerType = "P"}, HttpStatusCode.BadRequest, "The provided sessionId is invalid"},
            new object[]
                {new AddPlayer {SessionId = Guid.Empty, PlayerType = "K"}, HttpStatusCode.BadRequest, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}