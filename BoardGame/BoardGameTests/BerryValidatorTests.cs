using System.Collections.Generic;
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
        [InlineData("B 1 0", 4, true, "")]
        [InlineData("B 2 1", 4, true, "")]
        [InlineData("B 2 3", 4, true, "")]
        [InlineData("B 7 9", 10, true, "")]
        [InlineData("B 26 29", 30, true, "")]
        [InlineData("B 0 0", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 1 1", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 2 2", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 12 12", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 4 4", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("B 4 5", 5, false, "Input berry position should fit into the board size")]
        [InlineData("B 6 8", 5, false, "Input berry position should fit into the board size")]
        [InlineData("B 5 3", 5, false, "Input berry position should fit into the board size")]
        [InlineData("B 29 30", 30, false, "Input berry position should fit into the board size")]
        [InlineData("B X 30", 30, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 8 X", 30, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B1 1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 1 12 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 1 X", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 1 !", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 2 aaa", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B zxc 1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 1X1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 3 4 4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 3 4 4 4 4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData(" B 3 4", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("B 3 4 ", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData(" B 3 4 ", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("B1122", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B -1 -1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B -1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 2 -1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B -2 -4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B -2 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("B 1  2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 0 1", 5, true, "")]
        [InlineData("S 1 0", 5, true, "")]
        [InlineData("S 2 1", 5, true, "")]
        [InlineData("S 1 2", 5, true, "")]
        [InlineData("S 3 4", 5, true, "")]
        [InlineData("S 3 2", 5, true, "")]
        [InlineData("S 4 3", 5, true, "")]
        [InlineData("S 1 0", 4, true, "")]
        [InlineData("S 2 1", 4, true, "")]
        [InlineData("S 2 3", 4, true, "")]
        [InlineData("S 7 9", 10, true, "")]
        [InlineData("S 26 29", 30, true, "")]
        [InlineData("BS 1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("SB 1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 0 0", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("S 1 1", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("S 2 2", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("S 12 12", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("S 4 4", 5, false, "Input cannot be player starting position, for example: 'B 0 0'")]
        [InlineData("S 4 5", 5, false, "Input berry position should fit into the board size")]
        [InlineData("S 6 8", 5, false, "Input berry position should fit into the board size")]
        [InlineData("S 5 3", 5, false, "Input berry position should fit into the board size")]
        [InlineData("S 29 30", 30, false, "Input berry position should fit into the board size")]
        [InlineData("S X 30", 30, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 8 X", 30, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S1 1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 1 12 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 1 X", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 1 !", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 2 aaa", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S zxc 1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 1X1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 3 4 4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 3 4 4 4 4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData(" S 3 4", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("S 3 4 ", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData(" S 3 4 ", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("S1122", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S -1 -1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S -1 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 2 -1", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S -2 -4", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S -2 2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("S 1  2", 5, false, "Input should have the following syntax: 'B 1 2' or 'S 1 2'")]
        [InlineData("1 1", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("V 1 1", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData("X 1 1", 5, false, "Input should start with 'B' or 'S'")]
        [InlineData(null, 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData("", 5, false, "Input cannot be null, empty or whitespace")]
        [InlineData(" ", 5, false, "Input cannot be null, empty or whitespace")]
        public void ReturnExpectedVersusActualForDifferentInputInstructionsWithReason(string instruction, int boardSize, bool expectedResult, string expectedReason)
        {
            var walls = new List<Wall>();
            var actual = _validator.ValidateBerryInputWithReason(instruction, boardSize, walls);
            Assert.Equal(expectedResult, actual.IsValid);
            Assert.Equal(expectedReason, actual.Reason);
        }
    }
}