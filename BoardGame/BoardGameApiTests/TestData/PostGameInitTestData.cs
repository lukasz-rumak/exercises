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
                {new Board {WithSize = 2}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 3}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 5}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 10}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 18}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 19}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = 20}, HttpStatusCode.OK, "The game started"},
            new object[]
                {new Board {WithSize = -25}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = -5}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = -2}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = -1}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 0}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 1}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 21}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 22}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 30}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 300}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board {WithSize = 3000}, HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"},
            new object[]
                {new Board(), HttpStatusCode.BadRequest, "Please enter valid board size from 2 to 20"}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}