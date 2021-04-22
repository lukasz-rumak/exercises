using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using BoardGameApi.Models;
using BoardGameApiTests.Models;

namespace BoardGameApiTests.TestData
{
    public class PutNewBerryBadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 4 5"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"The berry was not created! Input berry position should fit into the board size"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 6 4"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"The berry was not created! Input berry position should fit into the board size"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 1 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates() { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "1 2"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B X 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "B 1 2 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "S 4 5"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"The berry was not created! Input berry position should fit into the board size"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "S 6 4"}, HttpStatusCode.BadRequest, new BadRequestErrors { Errors = new [] {"The berry was not created! Input berry position should fit into the board size"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "S 1 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates() { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "2 3"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "S X 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = "S 1 2 1"}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = ""}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"The BerryCoordinates field is required.", "Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = " "}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"The BerryCoordinates field is required.", "Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}},
            new object[]
                {Guid.Parse("c5665f24-93f5-4b55-81a0-8e245a9caecb"), new Berry {BerryCoordinates = null}, HttpStatusCode.BadRequest, new BadRequestBerryCoordinates { BerryCoordinates = new [] {"The BerryCoordinates field is required.", "Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"}}}
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}