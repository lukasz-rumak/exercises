using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;
using BoardGameApiTests.Models;

namespace BoardGameApiTests.TestData
{
    public class PutNewWallBadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 1 2 3 3"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 1 5 3 6"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W -1 2 0 2"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "1 2 1 3"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W X 1 1 3"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = "W 1 2 1 3 1"}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = ""}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"The WallCoordinates field is required.", "Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = " "}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"The WallCoordinates field is required.", "Please enter valid wall coordinates format. For example: W 1 1 2 1"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Wall {WallCoordinates = null}, HttpStatusCode.BadRequest, new BadRequestWallCoordinates { WallCoordinates = new [] {"The WallCoordinates field is required.", "Please enter valid wall coordinates format. For example: W 1 1 2 1"}}}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}