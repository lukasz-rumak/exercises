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
        [InlineData("W1 1 2 2", 5, false)]
        [InlineData("W 1 12 2", 5, false)]
        [InlineData("W 1 X 2 2", 5, false)]
        [InlineData("1 1 2 2", 5, false)]
        [InlineData("W 1 1 3 2", 5, false)]
        [InlineData("W 1 1 2 3", 5, false)]
        [InlineData("W 1X1 2 2", 5, false)]
        [InlineData("W 4 4 4 5", 5, false)]
        [InlineData("W 3 4 4 4", 5, true)]
        [InlineData("W 3 4 4", 5, false)]
        [InlineData("W 3 4 4 4 4", 5, false)]
        public void ReturnExpectedVersusActualForDifferentWallInputInstructions(string instruction, int boardSize, bool expectedResult)
        {
            var actual = _validator.ValidateWallsInput(instruction, boardSize);
            Assert.Equal(expectedResult, actual);
        }
    }
}