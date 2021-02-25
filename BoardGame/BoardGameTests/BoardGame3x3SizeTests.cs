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
            _game = new GameMaster(new Validator(),
                new BoardBuilder(new EventHandler(new ConsoleOutput()), new Validator()).WithSize(3).BuildBoard(),
                new Player(), new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"2 2 North"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 2 North"})]
        [InlineData(new []{"PRMMMMM"}, new []{"2 0 East"})]
        [InlineData(new []{"PRMRRMMMM"}, new []{"0 0 West"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"0 2 North"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"0 2 East"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"0 2 West"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"2 2 East"})]
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