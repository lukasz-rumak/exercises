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
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = "P"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = "K"}, HttpStatusCode.Created, "Created"},
            new object[]
                {new AddPlayer {SessionId = Guid.Empty, PlayerType = "P"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = "P"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = "X"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = ""}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = " "}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new AddPlayer {SessionId = Guid.NewGuid(), PlayerType = null}, HttpStatusCode.InternalServerError, ""}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}