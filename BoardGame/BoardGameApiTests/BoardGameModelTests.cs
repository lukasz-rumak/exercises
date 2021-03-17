using System;
using System.Linq;
using BoardGameApi.Models;
using BoardGameApiTests.Helpers;
using FluentAssertions;
using Xunit;

namespace BoardGameApiTests
{
    public class BoardGameModelTests
    {
        private readonly TestHelper _testHelper;

        public BoardGameModelTests()
        {
            _testHelper = new TestHelper();
        }

        [Theory]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "p", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "k", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "PP", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "KK", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "x", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "xx", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "X", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "XX", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "!", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "!!", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "!!!", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "", new [] {"The PlayerType field is required."})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", " ", new [] {"The PlayerType field is required."})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "  ", new [] {"The PlayerType field is required."})]
        public void AddPlayer_Model_PlayerType_Should_Return_Error(string sessionId, string playerType, string[] expectedResult)
        {
            var model = new AddPlayer {SessionId = Guid.Parse(sessionId), PlayerType = playerType};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
    }
}