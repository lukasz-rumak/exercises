using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class WallValidatorTests
    {
        private readonly IValidatorWall _validator;
        
        public WallValidatorTests()
        {
            _validator = new Validator();
        }

        [Theory]
        [InlineData("W 1 1 2 2", 5, true, "")]
        [InlineData("W 3 4 4 4", 5, true, "")]
        [InlineData("W 2 2 3 2", 5, true, "")]
        [InlineData("W 3 3 3 2", 5, true, "")]
        [InlineData("W 25 25 25 26", 30, true, "")]
        [InlineData("W 25 25 25 27", 30, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 29 29 29 30", 30, false, "Input wall position should fit into the board size")]
        [InlineData("W 29 29 X 30", 30, false, "Input wall position should be integers")]
        [InlineData("W 1 1 2 4", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 4 4 5 5", 5, false, "Input wall position should fit into the board size")]
        [InlineData("W 1 1 4 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 4 1 2 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 1 4 2 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 1 6 2 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W1 1 2 2", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData("W 1 12 2", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData("W 1 X 2 2", 5, false, "Input wall position should be integers")]
        [InlineData("W 1 22 2 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 1 ! 2 2", 5, false, "Input wall position should be integers")]
        [InlineData("W 1 1 2 aaa", 5, false, "Input wall position should be integers")]
        [InlineData("W zxc 1 2 1", 5, false, "Input wall position should be integers")]
        [InlineData("1 1 2 2", 5, false, "Input should start with 'W'")]
        [InlineData("V 1 1 2 2", 5, false, "Input should start with 'W'")]
        [InlineData("X 1 1 2 2", 5, false, "Input should start with 'W'")]
        [InlineData("W 1 1 3 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 1 1 2 3", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData("W 1X1 2 2", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData("W 4 4 4 5", 5, false, "Input wall position should fit into the board size")]
        [InlineData("W 3 4 4", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData("W 3 4 4 4 4", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData(" W 3 4 4 4", 5, false, "Input should start with 'W'")]
        [InlineData("W 3 4 4 4 ", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData(" W 3 4 4 4 ", 5, false, "Input should start with 'W'")]
        [InlineData("W1122", 5, false, "Input wall position should contain four chars divided by whitespace")]
        [InlineData("W -1 -1 -2 -2", 5, false, "Input wall position should fit into the board size")]
        [InlineData("W -1 -1 2 2", 5, false, "Input wall position difference should be 0 or 1")]
        [InlineData(null, 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData("", 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData(" ", 5, false, "Input cannot be null, empty or whitespace")]
        public void ReturnExpectedVersusActualForDifferentInputInstructionsWithReason(string instruction, int boardSize, bool expectedResult, string expectedReason)
        {
            var actual = _validator.ValidateWallInputWithReason(instruction, boardSize);
            Assert.Equal(expectedResult, actual.IsValid);
            Assert.Equal(expectedReason, actual.Reason);
        }
    }
}