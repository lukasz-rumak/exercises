using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;
using BoardGameApiTests.Models;

namespace BoardGameApiTests.TestData
{
    public class PutMovePlayerBadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 10, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"Event: incorrect player id! The requested player id: 10"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 100, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"Event: incorrect player id! The requested player id: 100"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 987654, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"Event: incorrect player id! The requested player id: 987654"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMXRXRXRM"}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMRR!"}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "!MMMRM"}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "XXX"}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = ""}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"The MoveTo field is required.", "Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = " "}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"The MoveTo field is required.", "Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = null}, HttpStatusCode.BadRequest, new BadRequestMoveTo { MoveTo = new [] {"The MoveTo field is required.", "Only following commands are available: M - move, L - turn left, R - turn right"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -1, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestPlayerId { PlayerId = new [] {"Please enter valid PlayerId as integer"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -11, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestPlayerId { PlayerId = new [] {"Please enter valid PlayerId as integer"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = -111, MoveTo = "MMMMMMMM"}, HttpStatusCode.BadRequest, new BadRequestPlayerId { PlayerId = new [] {"Please enter valid PlayerId as integer"}}}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}