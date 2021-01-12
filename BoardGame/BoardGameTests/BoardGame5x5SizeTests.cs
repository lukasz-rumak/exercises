using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame5x5SizeTests
    {
        private readonly IGame _game;
        
        public BoardGame5x5SizeTests()
        {
            _game = new GameManager(new Validator(), new BoardBuilder(5), new Pawn(5));
        }

        [Theory]
        [InlineData("MRMLMRM", "2 2 East")]
        [InlineData("RMMMLMM", "3 2 North")]
        [InlineData("MMMMM", "0 4 North")]
        [InlineData("MMMMMMMMM", "0 4 North")]
        [InlineData("MMMMMMMMMR", "0 4 East")]
        [InlineData("MMMMMMMMML", "0 4 West")]
        [InlineData("MMMMMMMMMRMMMMMMM", "4 4 East")]
        [InlineData("RMMMMMMMMMM", "4 0 East")]
        [InlineData("RMMLMMRMMMMMMMM", "4 2 East")]
        [InlineData("RMMLMMLMMMMMMMM", "0 2 West")]
        [InlineData("LMMMMMMMMMM", "0 0 West")]
        [InlineData("LLMMMMMMMMMM", "0 0 South")]
        [InlineData("MLMMM", "0 1 West")]
        [InlineData("RMLMLMMMMM", "0 1 West")]
        [InlineData("MXMMM", "Instruction not clear. Exiting...")]
        [InlineData("!@#$%^&*()", "Instruction not clear. Exiting...")]
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