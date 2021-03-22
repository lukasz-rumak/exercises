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
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        public void Board_Model_PlayerType_Should_Return_Empty(int withSize)
        {
            var model = new Board {WithSize = withSize};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-20 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(-10 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(-5 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(-1 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(0 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(1 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(21 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(22 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(23 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(30 ,new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(50, new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(100, new [] {"Please enter valid board size from 2 to 20"})]
        [InlineData(1000, new [] {"Please enter valid board size from 2 to 20"})]
        public void Board_Model_PlayerType_Should_Return_Error(int withSize, string[] expectedResult)
        {
            var model = new Board {WithSize = withSize};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
        
        [Theory]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "P")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", "K")]
        public void AddPlayer_Model_PlayerType_Should_Return_Empty(string sessionId, string playerType)
        {
            var model = new AddPlayer {SessionId = Guid.Parse(sessionId), PlayerType = playerType};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
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
        
        [Theory]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "M")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "L")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "R")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "RR")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "LLM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MMRMMLMM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 1, "MMRMMLMM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 10, "MMRMMLMM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 5555, "MMRMMLMM")]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 5555, "MMRMMLMMRRLLLLMMM")]
        public void MovePlayer_Model_PlayerId_And_MoveTo_Should_Return_Empty(string sessionId, int playerId, string moveTo)
        {
            var model = new MovePlayer {SessionId = Guid.Parse(sessionId), PlayerId = playerId, MoveTo = moveTo};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -1, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -10, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -100, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MMRMXMLMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MMRMML!MM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MMcRMMLMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "MMRMMLxMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "!", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "X!!!", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "XXX", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -1, "MMRMMXLMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -11, "MMR!MMLMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", -1111, "MMRMMLaMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "", new [] {"The MoveTo field is required."})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, " ", new [] {"The MoveTo field is required."})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, "  ", new [] {"The MoveTo field is required."})]
        [InlineData("c5665f24-93f5-4b55-81a0-8e245a9caecb", 0, null, new [] {"The MoveTo field is required."})]
        public void MovePlayer_Model_PlayerId_And_MoveTo_Should_Return_Error(string sessionId, int playerId, string moveTo, string[] expectedResult)
        {
            var model = new MovePlayer {SessionId = Guid.Parse(sessionId), PlayerId = playerId, MoveTo = moveTo};
            var result = _testHelper.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
    }
}
