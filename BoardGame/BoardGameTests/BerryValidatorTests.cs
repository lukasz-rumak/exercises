using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BerryValidatorTests
    {
        private readonly IValidatorBerry _validator;
        
        public BerryValidatorTests()
        {
            _validator = new Validator();
        }

        [Theory]
        [InlineData("B 0 1", 5, true, "")]
        [InlineData("B 1 0", 5, true, "")]
        [InlineData("B 2 1", 5, true, "")]
        [InlineData("B 1 2", 5, true, "")]
        [InlineData("B 3 4", 5, true, "")]
        [InlineData("B 3 2", 5, true, "")]
        [InlineData("B 4 3", 5, true, "")]
        [InlineData("B 26 29", 30, true, "")]
        [InlineData("B 0 0", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 1 1", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 2 2", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 3 3", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 4 4", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 29 30", 30, false, "Input wall position should fit into the board size")]
        [InlineData("B X 30", 30, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 4 5", 5, false, "Input wall position should fit into the board size")]
        [InlineData("B1 1", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 1 12 2", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 1 X", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 1 !", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 2 aaa", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B zxc 1", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("1 1", 5, false, "Input should start with 'B'")]
        [InlineData("V 1 1", 5, false, "Input should start with 'B'")]
        [InlineData("X 1 1", 5, false, "Input should start with 'B'")]
        [InlineData("B 1X1 2", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 3 4 4", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B 3 4 4 4 4", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData(" B 3 4", 5, false, "Input should start with 'B'")]
        [InlineData("B 3 4 4 4 ", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData(" B 3 4 4 4 ", 5, false, "Input should start with 'B'")]
        [InlineData("B1122", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData("B -1 -1", 5, false, "Input should have the following syntax: 'B 1 2'")]
        [InlineData(null, 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData("", 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData(" ", 5, false, "Input cannot be null, empty or whitespace")]
        public void ReturnExpectedVersusActualForDifferentWallInputInstructionsWithReason(string instruction, int boardSize, bool expectedResult, string expectedReason)
        {
            var actual = _validator.ValidateBerryInputWithReason(instruction, boardSize);
            Assert.Equal(expectedResult, actual.IsValid);
            Assert.Equal(expectedReason, actual.Reason);
        }
    }
}