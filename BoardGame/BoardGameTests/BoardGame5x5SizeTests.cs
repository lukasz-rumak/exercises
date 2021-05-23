using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame5x5SizeTests
    {
        private readonly IGame _game;

        public BoardGame5x5SizeTests()
        {
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(),
                new Validator(), new Validator(), new PlayersHandler(), new BerryCreator(), new AStarPathFinder());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(),
                _game.ObjectFactory.Get<IBerryCreator>(), _game.ObjectFactory.Get<IAStarPathFinder>()).WithSize(5).BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"[P] 2 2 East [S] 0"})]
        [InlineData(new []{"PMRMLMRM", "PRMMMLMMM"}, new []{"[P] 2 2 East [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"KMRMLMRM", "KRMMMLMMM"}, new []{"[P] 2 0 SouthEast [S] 0", "[P] 4 2 NorthEast [S] 0"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"[P] 3 2 North [S] 0"})]
        [InlineData(new []{"PRMMMLMM", "PMRMLMRM"}, new []{"[P] 3 2 North [S] 0", "[P] 3 3 East [S] 0"})]
        [InlineData(new []{"PMMMMM"}, new []{"[P] 0 4 North [S] 0"})]
        [InlineData(new []{"PMMMMM", "PRMMMLMMM"}, new []{"[P] 0 4 North [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"[P] 0 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMM", "PRMMMLMMM"}, new []{"[P] 0 4 North [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"[P] 0 4 East [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMR", "PRMMMLMMM"}, new []{"[P] 0 4 East [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"[P] 0 4 West [S] 0"})]
        [InlineData(new []{"PMMMMMMMMML", "PRMMMLMMM"}, new []{"[P] 0 4 West [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"[P] 4 4 East [S] 0"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM", "PMMMMMMMMMRMMMMMMMM"}, new []{"[P] 3 4 East [S] 0", "[P] 4 4 East [S] 0"})]
        [InlineData(new []{"PRMMMMMMMMMM"}, new []{"[P] 4 0 East [S] 0"})]
        [InlineData(new []{"PRMMMMMMMMMM", "PRMMMMMMMMMMM"}, new []{"[P] 4 0 East [S] 0", "[P] 4 1 East [S] 0"})]
        [InlineData(new []{"PRMMLMMRMMMMMMMM"}, new []{"[P] 4 2 East [S] 0"})]
        [InlineData(new []{"PRMMLMMRMMMMMMMM", "PMXMMM"}, new []{"[P] 4 2 East [S] 0", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PRMMLMMLMMMMMMMM"}, new []{"[P] 0 2 West [S] 0"})]
        [InlineData(new []{"PRMMLMMLMMMMMMMM", "PMXMMM"}, new []{"[P] 0 2 West [S] 0", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PLMMMMMMMMMM"}, new []{"[P] 0 0 West [S] 0"})]
        [InlineData(new []{"PLMMMMMMMMMM", "PRMMLMMRMMMMMMMMM"}, new []{"[P] 0 0 West [S] 0", "[P] 4 3 East [S] 0"})]
        [InlineData(new []{"PLLMMMMMMMMMM"}, new []{"[P] 0 0 South [S] 0"})]
        [InlineData(new []{"PLLMMMMMMMMMM", "PMLMMMM"}, new []{"[P] 0 0 South [S] 0", "[P] 0 2 West [S] 0"})]
        [InlineData(new []{"PMLMMM"}, new []{"[P] 0 1 West [S] 0"})]
        [InlineData(new []{"PMLMMM", "PMLMMMM"}, new []{"[P] 0 1 West [S] 0", "[P] 0 2 West [S] 0"})]
        [InlineData(new []{"PRMLMLMMMMM"}, new []{"[P] 0 1 West [S] 0"})]
        [InlineData(new []{"PRMLMLMMMMM", "PRMLMLMMMMMM"}, new []{"[P] 0 1 West [S] 0", "[P] 0 2 West [S] 0"})]
        [InlineData(new []{"PRMMMMLMMMM", "PRRLMMMMLMMMM"}, new []{"[P] 4 1 North [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMMMMMMMMM", "PMMMMMMMMM"}, new []{"[P] 0 4 North [S] 0", "[P] 1 4 North [S] 0"})]
        [InlineData(new []{"PMMMM", "PRMMMM"}, new []{"[P] 0 4 North [S] 0", "[P] 4 1 East [S] 0"})]
        [InlineData(new []{"PM","PM","PM","PM","PM","PM","PM","PM","PM","PM","PM"}, new []{"[P] 0 1 North [S] 0", "[P] 1 2 North [S] 0", "[P] 2 3 North [S] 0", "[P] 3 4 North [S] 0", "[P] 4 4 North [S] 0"})]
        [InlineData(new []{"PMXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMXRRR"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMRMLMRM", "PMXRRR"}, new []{"[P] 1 2 East [S] 0", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "[P] 3 3 East [S] 0"})]
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