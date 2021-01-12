using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame8x8SizeTests
    {
        private readonly IGame _game;
        
        public BoardGame8x8SizeTests()
        {
            _game = new GameManager(new Validator(), new BoardBuilder(8), new Pawn(8));
        }

        [Theory]
        [InlineData("MRMLMRM", "2 2 East")]
        [InlineData("RMMMLMM", "3 2 North")]
        [InlineData("MMMMM", "0 5 North")]
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