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
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "P"}, HttpStatusCode.Created, "Created"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "K"}, HttpStatusCode.Created, "Created"}, 
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "PP"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "X"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "Y"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "XX"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "!"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = ""}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = " "}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = null}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Empty, new AddPlayer {PlayerType = "P"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.Empty, new AddPlayer {PlayerType = "K"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.NewGuid(), new AddPlayer {PlayerType = "P"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.NewGuid(), new AddPlayer {PlayerType = "K"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}