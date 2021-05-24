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
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(),
                new Validator(), new Validator(), new PlayersHandler(), new BerryCreator(), new AStarPathFinderAlgorithm());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(),
                _game.ObjectFactory.Get<IBerryCreator>(), _game.ObjectFactory.Get<IAStarPathFinderAlgorithm>()).WithSize(8).BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"[P] 2 2 East [S] 0"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"[P] 3 2 North [S] 0"})]
        [InlineData(new []{"PMMMMM"}, new []{"[P] 0 5 North [S] 0"})]
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