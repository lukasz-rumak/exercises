using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame2x2SizeTests
    {
        private readonly IGame _game;
        
        public BoardGame2x2SizeTests()
        {
            _game = new GameManager(new Validator(), new BoardBuilder(2), new Pawn(2));
        }

        [Theory]
        [InlineData("MRMLMRM", "1 1 East")]
        [InlineData("RMMMLMM", "1 1 North")]
        [InlineData("MMMMM", "0 1 North")]
        [InlineData("MMMMMMMMM", "0 1 North")]
        [InlineData("MMMMMMMMMR", "0 1 East")]
        [InlineData("MMMMMMMMML", "0 1 West")]
        [InlineData("MMMMMMMMMRMMMMMMM", "1 1 East")]
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