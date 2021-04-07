using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PutMovePlayerOkAndNotFound : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMRRRRM"}, HttpStatusCode.OK, "Piece moved. PieceId: 0, PieceType: Pawn, new position: (0,4)"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "RMMMMLMM"}, HttpStatusCode.OK, "Piece moved. PieceId: 0, PieceType: Pawn, new position: (4,2)"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMLMMMRRMMMM"}, HttpStatusCode.OK, "Piece moved. PieceId: 0, PieceType: Pawn, new position: (4,3)"},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.OK, "Move not possible (outside of the boundaries)! (0, 5)"},
            new object[]
                {Guid.Empty, new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"},
            new object[]
                {Guid.NewGuid(), new MovePlayer {PlayerId = 0, MoveTo = "MMMMMMMM"}, HttpStatusCode.NotFound, "The provided sessionId is invalid"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}