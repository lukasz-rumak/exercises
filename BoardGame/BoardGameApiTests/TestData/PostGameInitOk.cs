using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;

namespace BoardGameApiTests.TestData
{
    public class PostGameInitOk : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new Board {WithSize = 2}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 3}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 5}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 10}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 18}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 19}, HttpStatusCode.OK, "The game has started"},
            new object[]
                {new Board {WithSize = 20}, HttpStatusCode.OK, "The game has started"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}