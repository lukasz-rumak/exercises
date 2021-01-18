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
            _game = new GameMaster(new Validator(), new BoardBuilder().WithSize(2).GenerateBoard().Build());
        }

        [Theory]
        [InlineData(new []{"MRMLMRM"}, new []{"1 1 East"})]
        [InlineData(new []{"RMMMLMM"}, new []{"1 1 North"})]
        [InlineData(new []{"MMMMM"}, new []{"0 1 North"})]
        [InlineData(new []{"MMMMMMMMM"}, new []{"0 1 North"})]
        [InlineData(new []{"MMMMMMMMMR"}, new []{"0 1 East"})]
        [InlineData(new []{"MMMMMMMMML"}, new []{"0 1 West"})]
        [InlineData(new []{"MMMMMMMMMRMMMMMMM"}, new []{"1 1 East"})]
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