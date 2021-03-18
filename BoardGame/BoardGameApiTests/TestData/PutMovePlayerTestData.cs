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
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = "MMMMMMMM"}, HttpStatusCode.OK, "Moved to 0 4 North"},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = "RMMMMLMM"}, HttpStatusCode.OK, "Moved to 4 2 North"},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = "MMMLMMMRRMMMM"}, HttpStatusCode.OK, "Moved to 4 3 East"},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 10, Move = "MMMMMMMM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 100, Move = "MMMMMMMM"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = "XXX"}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = ""}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = " "}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = 0, Move = null}, HttpStatusCode.BadRequest, null},
            new object[]
                {new MovePlayer {SessionId = Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), PlayerId = -1, Move = "MMMMMMMM"}, HttpStatusCode.BadRequest, null}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}