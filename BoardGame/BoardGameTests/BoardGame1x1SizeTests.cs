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
            _game = new GameMaster(new Validator(),
                new BoardBuilder(new EventHandler(new ConsoleOutput()), new Validator()).WithSize(1).BuildBoard(),
                new Player(), new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"0 0 East"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"0 0 East"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"0 0 West"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"0 0 East"})]
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