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
            _game = new GameMaster(new Validator(), new BoardBuilder().WithSize(8).GenerateBoard().Build(), new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"MRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"RMMMLMM"}, new []{"3 2 North"})]
        [InlineData(new []{"MMMMM"}, new []{"0 5 North"})]
        [InlineData(new []{"MXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{""}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{" "}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(null, new []{"Instruction not clear. Exiting..."})]
        public void ReturnExpectedVResultForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}