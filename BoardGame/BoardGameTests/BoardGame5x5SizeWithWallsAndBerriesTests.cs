using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame5x5SizeWithWallsAndBerriesTests
    {
        private readonly IGame _game;

        public BoardGame5x5SizeWithWallsAndBerriesTests()
        {
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(), new Validator(), new Validator(), new PlayersHandler(), new BerryCreator());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                    _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(), _game.ObjectFactory.Get<IBerryCreator>()).WithSize(5)
                .AddWall("W 0 0 1 1").AddWall("W 3 3 4 4").AddWall("W 2 2 3 3")
                .AddWall("W 1 1 1 2").AddWall("W 0 3 0 4").AddWall("W 3 4 4 4")
                .AddBerry("B 0 0").AddBerry("S 0 1").AddBerry("B 0 2").AddBerry("S 0 3")
                .AddBerry("B 1 0").AddBerry("S 1 1").AddBerry("B 1 2").AddBerry("S 1 3")
                .AddBerry("B 2 0").AddBerry("S 2 1").AddBerry("B 2 2").AddBerry("S 2 3")
                .AddBerry("B 3 0").AddBerry("S 3 1").AddBerry("B 3 2").AddBerry("S 3 3")
                .BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PMMMMMM", "PMMMMMM"}, new []{"[P] 0 3 North [S] 5", "[P] 1 1 North [S] 0"})]
        [InlineData(new []{"PMMMMMM", "PRMMMMM"}, new []{"[P] 0 3 North [S] 5", "[P] 4 1 East [S] 4"})]
        [InlineData(new []{"PRMLMRMLMRMLMRMLM"}, new []{"[P] 4 4 North [S] 4"})]
        [InlineData(new []{"PRMLMRMLMRMLM", "PRMLMRMLMRMLMRMLM"}, new []{"[P] 3 3 North [S] 1", "[P] 4 4 North [S] 3"})]
        [InlineData(new []{"KMMMMM", "KMMMMM"}, new []{"[P] 0 0 NorthEast [S] 0", "[P] 2 2 NorthEast [S] 0"})]
        [InlineData(new []{"PRMMMLMLMMMM"}, new []{"[P] 0 1 West [S] 9"})]
        [InlineData(new []{"PRMMMLMLMMMM", "PMRMMMLMMMLMMMMM"}, new []{"[P] 0 1 West [S] 5", "[P] 4 4 West [S] 4"})]
        [InlineData(new []{"PRMMMMLMMMMLMMMMM"}, new []{"[P] 4 4 West [S] 3"})]
        [InlineData(new []{"PMXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMXRRR"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMRMLMRM", "PMXRRR"}, new []{"[P] 1 2 East [S] 4", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "[P] 3 2 East [S] 3"})]
        [InlineData(new []{"!@#$%^&*()"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"!@#$%^&*()", "!@#$%^&*()"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{""}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"", " "}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{" "}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{" ", ""}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(null, new []{"Instruction not clear. Exiting..."})]
        public void ReturnExpectedVersusActualForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}