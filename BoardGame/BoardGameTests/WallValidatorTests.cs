using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class WallValidatorTests
    {
        private readonly IValidator _validator;
        
        public WallValidatorTests()
        {
            _validator = new Validator();
        }

        [Theory]
        [InlineData("W 1 1 2 2", 5, true)]
        [InlineData("W 3 4 4 4", 5, true)]
        [InlineData("W 2 2 3 2", 5, true)]
        [InlineData("W 3 3 3 2", 5, true)]
        [InlineData("W 25 25 25 26", 30, true)]
        [InlineData("W 25 25 25 27", 30, false)]
        [InlineData("W 29 29 29 30", 30, false)]
        [InlineData("W 29 29 X 30", 30, false)]
        [InlineData("W 1 1 2 4", 5, false)]
        [InlineData("W 4 4 5 5", 5, false)]
        [InlineData("W 1 1 4 2", 5, false)]
        [InlineData("W 4 1 2 2", 5, false)]
        [InlineData("W 1 4 2 2", 5, false)]
        [InlineData("W 1 6 2 2", 5, false)]
        [InlineData("W1 1 2 2", 5, false)]
        [InlineData("W 1 12 2", 5, false)]
        [InlineData("W 1 X 2 2", 5, false)]
        [InlineData("W 1 22 2 2", 5, false)]
        [InlineData("W 1 ! 2 2", 5, false)]
        [InlineData("W 1 1 2 aaa", 5, false)]
        [InlineData("W zxc 1 2 1", 5, false)]
        [InlineData("1 1 2 2", 5, false)]
        [InlineData("V 1 1 2 2", 5, false)]
        [InlineData("W 1 1 3 2", 5, false)]
        [InlineData("W 1 1 2 3", 5, false)]
        [InlineData("W 1X1 2 2", 5, false)]
        [InlineData("W 4 4 4 5", 5, false)]
        [InlineData("W 3 4 4", 5, false)]
        [InlineData("W 3 4 4 4 4", 5, false)]
        [InlineData(" W 3 4 4 4", 5, false)]
        [InlineData("W 3 4 4 4 ", 5, false)]
        [InlineData(" W 3 4 4 4 ", 5, false)]
        public void ReturnExpectedVersusActualForDifferentWallInputInstructions(string instruction, int boardSize, bool expectedResult)
        {
            var actual = _validator.ValidateWallsInput(instruction, boardSize);
            Assert.Equal(expectedResult, actual);
        }
    }
}