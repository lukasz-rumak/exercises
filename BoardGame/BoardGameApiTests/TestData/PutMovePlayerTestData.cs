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
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.OK, "Moved to 0 4 North"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "RMMMMLMM"}, HttpStatusCode.OK, "Moved to 4 2 North"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMLMMMRRMMMM"}, HttpStatusCode.OK, "Moved to 4 3 East"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 10, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, "Event: incorrect player id! The requested player id: 10"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 100, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, "Event: incorrect player id! The requested player id: 100"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 987654, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, "Event: incorrect player id! The requested player id: 987654"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMXRXRXRM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMRR!"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "!MMMRM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "XXX"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = ""}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = " "}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = null}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -1, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -11, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -111, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {Guid.Empty, new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.NewGuid(), new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}