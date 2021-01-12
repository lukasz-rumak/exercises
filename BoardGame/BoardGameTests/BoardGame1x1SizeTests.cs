using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame1x1SizeTests
    {
        private readonly IGame _game;
        
        public BoardGame1x1SizeTests()
        {
            _game = new GameManager(new Validator(), new BoardBuilder(1), new Pawn(1));
        }

        [Theory]
        [InlineData("MRMLMRM", "0 0 East")]
        [InlineData("RMMMLMM", "0 0 North")]
        [InlineData("MMMMM", "0 0 North")]
        [InlineData("MMMMMMMMM", "0 0 North")]
        [InlineData("MMMMMMMMMR", "0 0 East")]
        [InlineData("MMMMMMMMML", "0 0 West")]
        [InlineData("MMMMMMMMMRMMMMMMM", "0 0 East")]
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