using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PutMovePlayerTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = 0, Move = "MMMMMMMM"}, HttpStatusCode.OK, "Moved to 0 3 North"},
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = 0, Move = "XXX"}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = 0, Move = ""}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = 0, Move = " "}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = 0, Move = null}, HttpStatusCode.InternalServerError, ""},
            new object[]
                {new MovePlayer {SessionId = Guid.NewGuid(), PlayerId = -1, Move = "MMMMMMMM"}, HttpStatusCode.InternalServerError, ""},
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}