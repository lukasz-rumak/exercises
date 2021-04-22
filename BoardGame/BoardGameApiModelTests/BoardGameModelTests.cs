using System.Linq;
using BoardGameApi.Models;
using BoardGameApiModelTests.Helpers;
using FluentAssertions;
using Xunit;

namespace BoardGameApiModelTests
{
    public class BoardGameModelTests
    {
        private readonly ModelValidator _modelValidator;

        public BoardGameModelTests()
        {
            _modelValidator = new ModelValidator();
        }
        
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        public void Board_Model_WithSize_Should_Return_Empty(int withSize)
        {
            var model = new Board {WithSize = withSize};
            var result = _modelValidator.ValidateModel(model);
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
        public void Board_Model_WithSize_Should_Return_Error(int withSize, string[] expectedResult)
        {
            var model = new Board {WithSize = withSize};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
        
        [Theory]
        [InlineData("B 0 1")]
        [InlineData("B 1 0")]
        [InlineData("B 1 2")]
        [InlineData("B 2 1")]
        [InlineData("B 14 555")]
        [InlineData("B 555 777")]
        [InlineData("B 8888 9999")]
        [InlineData("S 0 1")]
        [InlineData("S 1 0")]
        [InlineData("S 1 2")]
        [InlineData("S 2 1")]
        [InlineData("S 14 555")]
        [InlineData("S 555 777")]
        [InlineData("S 8888 9999")]
        public void Berry_Model_BerryCoordinates_Should_Return_Empty(string berryCoordinates)
        {
            var model = new Berry {BerryCoordinates = berryCoordinates};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }

        [Theory]
        [InlineData("B 0 0", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1 1", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 12 12", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B X 30", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 8 X", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B1 1 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1 12 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1 X 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1 !", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1 aaa", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B zxc", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1X1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 3 4 4 4 4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData(" B 3 4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 3 4 ", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData(" B 3 4 ", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B11", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B -1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 2 -1", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B -2 -4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B -2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("B 1  2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 0 0", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1 1", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 12 12", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S X 30", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 8 X", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S1 1 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1 12 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1 X 2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1 !", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1 aaa", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S zxc", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1X1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 3 4 4 4 4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData(" S 3 4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 3 4 ", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData(" S 3 4 ", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S11", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S -1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 2 -1", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S -2 -4", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S -2 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("S 1  2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("V 1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData("X 1 2", new [] {"Please enter valid berry coordinates format. For example: 'B 1 2' or 'S 1 2'"})]
        [InlineData(null, new [] {"The BerryCoordinates field is required."})]
        [InlineData("", new [] {"The BerryCoordinates field is required."})]
        [InlineData(" ", new [] {"The BerryCoordinates field is required."})]
        public void Berry_Model_BerryCoordinates_Should_Return_Error(string berryCoordinates, string[] expectedResult)
        {
            var model = new Berry {BerryCoordinates = berryCoordinates};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
        
        [Theory]
        [InlineData("W 1 1 2 2")]
        [InlineData("W 3 4 4 4")]
        [InlineData("W 2 2 3 2")]
        [InlineData("W 3 3 3 2")]
        [InlineData("W 25 25 25 26")]
        [InlineData("W 255 255 255 256")]
        [InlineData("W 2555 2555 2555 2556")]
        public void Wall_Model_WallCoordinates_Should_Return_Empty(string wallCoordinates)
        {
            var model = new Wall {WallCoordinates = wallCoordinates};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }

        [Theory]
        [InlineData("W 25 25 25 27", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})] 
        [InlineData("W 29 29 X 30", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 2 4", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 4 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 4 1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 4 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 6 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W1 1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 12 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 X 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 22 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 ! 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 2 aaa", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 aaa 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W zxc 1 2 1", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("1 1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("V 1 1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("X 1 1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 3 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1 1 2 3", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1X1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 3 4 4", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 3 4 4 4 4", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData(" W 3 4 4 4", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 3 4 4 4 ", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData(" W 3 4 4 4 ", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W1122", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W -1 -1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData("W 1  1 2 2", new [] {"Please enter valid wall coordinates format. For example: 'W 1 1 2 1'"})]
        [InlineData(null, new [] {"The WallCoordinates field is required."})]
        [InlineData("", new [] {"The WallCoordinates field is required."})]
        [InlineData(" ", new [] {"The WallCoordinates field is required."})]
        public void Wall_Model_WallCoordinates_Should_Return_Error(string wallCoordinates, string[] expectedResult)
        {
            var model = new Wall {WallCoordinates = wallCoordinates};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
        
        [Theory]
        [InlineData("P")]
        [InlineData("K")]
        public void AddPlayer_Model_PlayerType_Should_Return_Empty(string playerType)
        {
            var model = new AddPlayer {PlayerType = playerType};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }

        [Theory]
        [InlineData("p", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("k", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("PP", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("KK", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("x", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("xx", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("X", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("XX", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("!", new [] {"The PlayerType must be 'P' or 'K'"})]
        [InlineData("!!", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("!!!", new [] {"The PlayerType must be 'P' or 'K'", "The PlayerType value must be exactly 1 character"})]
        [InlineData("", new [] {"The PlayerType field is required."})]
        [InlineData(" ", new [] {"The PlayerType field is required."})]
        [InlineData("  ", new [] {"The PlayerType field is required."})]
        public void AddPlayer_Model_PlayerType_Should_Return_Error(string playerType, string[] expectedResult)
        {
            var model = new AddPlayer {PlayerType = playerType};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
        
        [Theory]
        [InlineData(0, "M")]
        [InlineData(0, "L")]
        [InlineData(0, "R")]
        [InlineData(0, "MM")]
        [InlineData(0, "RR")]
        [InlineData(0, "LLM")]
        [InlineData(0, "MMRMMLMM")]
        [InlineData(1, "MMRMMLMM")]
        [InlineData(10, "MMRMMLMM")]
        [InlineData(5555, "MMRMMLMM")]
        [InlineData(5555, "MMRMMLMMRRLLLLMMM")]
        public void MovePlayer_Model_PlayerId_And_MoveTo_Should_Return_Empty(int playerId, string moveTo)
        {
            var model = new MovePlayer {PlayerId = playerId, MoveTo = moveTo};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData(-1, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData(-10, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData(-100, "MMRMMLMM", new [] {"Please enter valid PlayerId as integer"})]
        [InlineData(0, "MMRMXMLMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "MMRMML!MM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "MMcRMMLMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "MMRMMLxMM", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "!", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "X!!!", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "XXX", new [] {"Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(-1, "MMRMMXLMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(-11, "MMR!MMLMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(-1111, "MMRMMLaMM", new [] {"Please enter valid PlayerId as integer", "Only following commands are available: M - move, L - turn left, R - turn right"})]
        [InlineData(0, "", new [] {"The MoveTo field is required."})]
        [InlineData(0, " ", new [] {"The MoveTo field is required."})]
        [InlineData(0, "  ", new [] {"The MoveTo field is required."})]
        [InlineData(0, null, new [] {"The MoveTo field is required."})]
        public void MovePlayer_Model_PlayerId_And_MoveTo_Should_Return_Error(int playerId, string moveTo, string[] expectedResult)
        {
            var model = new MovePlayer {PlayerId = playerId, MoveTo = moveTo};
            var result = _modelValidator.ValidateModel(model);
            var resultErrorMessage = result.Select(x => x.ErrorMessage);
            resultErrorMessage.Should().Contain(expectedResult);
        }
    }
}
