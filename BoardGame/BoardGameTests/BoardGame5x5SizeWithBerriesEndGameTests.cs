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
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(), new Validator(), new Validator(), new PlayersHandler(), new BerryCreator());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                    _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(), _game.ObjectFactory.Get<IBerryCreator>()).WithSize(5)
                .AddBerry("B 0 0").AddBerry("S 1 0").AddBerry("B 2 0").AddBerry("B 3 0").AddBerry("B 4 0")
                .AddBerry("B 1 1").AddBerry("S 2 1").AddBerry("B 3 1").AddBerry("B 4 1")
                .BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PRMMMMLMMMMMM", "PRMMMMLMMMMMM"}, new []{"[P] 4 0 East [S] 5", "[P] 4 1 East [S] 4"})]
        public void ReturnExpectedVersusActualForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}