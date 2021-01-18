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
            _game = new GameMaster(new Validator(), new BoardBuilder().WithSize(1).GenerateBoard().Build());
        }

        [Theory]
        [InlineData(new []{"MRMLMRM"}, new []{"0 0 East"})]
        [InlineData(new []{"RMMMLMM"}, new []{"0 0 North"})]
        [InlineData(new []{"MMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"MMMMMMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"MMMMMMMMMR"}, new []{"0 0 East"})]
        [InlineData(new []{"MMMMMMMMML"}, new []{"0 0 West"})]
        [InlineData(new []{"MMMMMMMMMRMMMMMMM"}, new []{"0 0 East"})]
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