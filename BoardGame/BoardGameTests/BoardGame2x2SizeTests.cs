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
            _game = new GameMaster(new Validator(), new BoardBuilder().WithSize(2).BuildBoard(), new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"1 1 East"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"1 1 North"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 1 North"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"0 1 North"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"0 1 East"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"0 1 West"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"1 1 East"})]
        [InlineData(new []{"PM","PM","PM","PM","PM"}, new []{"0 1 North", "1 1 North"})]
        [InlineData(new []{"PMXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{""}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{" "}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(null, new []{"Instruction not clear. Exiting..."})]
        public void ReturnExpectedVersusActualForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}