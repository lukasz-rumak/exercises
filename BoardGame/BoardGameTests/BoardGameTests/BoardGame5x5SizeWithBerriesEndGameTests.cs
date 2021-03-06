using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame5x5SizeWithBerriesEndGameTests
    {
        private readonly IGame _game;

        public BoardGame5x5SizeWithBerriesEndGameTests()
        {
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(),
                new Validator(), new Validator(), new PlayersHandler(), new BerryCreator(), new AStarPathFinderAdapter(new AStarPathFinderAlgorithm(), new PlayersHandler()));
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                    _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(),
                    _game.ObjectFactory.Get<IBerryCreator>(), _game.ObjectFactory.Get<IAStarPathFinderAdapter>()).WithSize(5)
                .AddBerry("B 0 0").AddBerry("S 2 0").AddBerry("B 4 0")
                .AddBerry("B 1 1").AddBerry("S 3 1")
                .BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PRMMMMLMMMMMM", "PRMMMMLMMMMMM"}, new []{"[P] 4 0 East [S] 3", "[P] 4 1 East [S] 2"})]
        public void ReturnExpectedVersusActualForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}