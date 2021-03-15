using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PostGameInitTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                new Board {Wall = new Wall {WallCoordinates = "W 1 1 2 2"}, WithSize = 5},
                HttpStatusCode.OK, "Game started"
            },
            new object[]
            {
                new Board {Wall = new Wall {WallCoordinates = "W 1 1 2 6"}, WithSize = 5},
                HttpStatusCode.BadRequest, "Game started"
            },
            new object[] {null, HttpStatusCode.BadRequest, "Game started"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}