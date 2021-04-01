using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;
using BoardGameApiTests.Models;

namespace BoardGameApiTests.TestData
{
    public class PostGameInitBadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {new Board {WithSize = -25}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = -5}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = -2}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = -1}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 0}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 1}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 21}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 22}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 30}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 300}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board {WithSize = 3000}, HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}},
            new object[]
                {new Board(), HttpStatusCode.BadRequest, new BadRequestWithSize { WithSize = new [] {"Please enter valid board size from 2 to 20"}}}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}