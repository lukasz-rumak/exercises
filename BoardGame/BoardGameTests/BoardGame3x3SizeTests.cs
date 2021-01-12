using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame3x3SizeTests
    {
        private readonly IGame _game;
        
        public BoardGame3x3SizeTests()
        {
            _game = new GameManager(new Validator(), new BoardBuilder(3), new Pawn(3));
        }

        [Theory]
        [InlineData("MRMLMRM", "2 2 East")]
        [InlineData("RMMMLMM", "2 2 North")]
        [InlineData("MMMMM", "0 2 North")]
        [InlineData("RMMMMM", "2 0 East")]
        [InlineData("RMRRMMMM", "0 0 West")]
        [InlineData("MMMMMMMMM", "0 2 North")]
        [InlineData("MMMMMMMMMR", "0 2 East")]
        [InlineData("MMMMMMMMML", "0 2 West")]
        [InlineData("MMMMMMMMMRMMMMMMM", "2 2 East")]
        [InlineData("MXMMM", "Instruction not clear. Exiting...")]
        [InlineData("", "Instruction not clear. Exiting...")]
        [InlineData(" ", "Instruction not clear. Exiting...")]
        [InlineData(null, "Instruction not clear. Exiting...")]
        //[InlineData(new [] { 1, 2, 3, 4, 5 }, "Instruction not clear. Exiting...")]
        public void ReturnExpectedVResultForDifferentInputInstructions(string input, string expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}