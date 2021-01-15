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
            _game = new GameMaster(new Validator(), new BoardBuilder(3));
        }

        [Theory]
        [InlineData(new []{"MRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"RMMMLMM"}, new []{"2 2 North"})]
        [InlineData(new []{"MMMMM"}, new []{"0 2 North"})]
        [InlineData(new []{"RMMMMM"}, new []{"2 0 East"})]
        [InlineData(new []{"RMRRMMMM"}, new []{"0 0 West"})]
        [InlineData(new []{"MMMMMMMMM"}, new []{"0 2 North"})]
        [InlineData(new []{"MMMMMMMMMR"}, new []{"0 2 East"})]
        [InlineData(new []{"MMMMMMMMML"}, new []{"0 2 West"})]
        [InlineData(new []{"MMMMMMMMMRMMMMMMM"}, new []{"2 2 East"})]
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