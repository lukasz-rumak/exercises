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
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(), new Validator(), new Validator(), new PlayersHandler());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(), _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>()).WithSize(1).BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"[P] 0 0 East [S] 0"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"[P] 0 0 North [S] 0"})]
        [InlineData(new []{"PMMMMM"}, new []{"[P] 0 0 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"[P] 0 0 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"[P] 0 0 East [S] 0"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"[P] 0 0 West [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"[P] 0 0 East [S] 0"})]
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