using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;
using BoardGameApiTests.Models;

namespace BoardGameApiTests.TestData
{
    public class PostAddPlayerBadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "PP"}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType value must be exactly 1 character", "The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "X"}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "Y"}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "XX"}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType value must be exactly 1 character", "The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "!"}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = ""}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType field is required.", "The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = " "}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType field is required.", "The PlayerType must be 'P' or 'K'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = "  "}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType field is required.", "The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new AddPlayer {PlayerType = null}, HttpStatusCode.BadRequest, new BadRequestPlayerType { PlayerType = new [] {"The PlayerType field is required.", "The PlayerType must be 'P' or 'K'"}}}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}