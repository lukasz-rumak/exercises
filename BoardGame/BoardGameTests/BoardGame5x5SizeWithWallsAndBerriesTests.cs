using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame5x5SizeWithWallsAndBerriesTests
    {
        private readonly IGame _game;

        public BoardGame5x5SizeWithWallsAndBerriesTests()
        {
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(), new Validator(), new Validator(), new PlayersHandler());
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(), _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>()).WithSize(5)
                    .AddWall("W 0 0 1 1").AddWall("W 3 3 4 4").AddWall("W 2 2 3 3")
                    .AddWall("W 1 1 1 2").AddWall("W 0 3 0 4").AddWall("W 3 4 4 4")
                    .AddBerry("B 0 1").AddBerry("B 0 2").AddBerry("B 1 1").AddBerry("B 1 2")
                    .AddBerry("B 2 1").AddBerry("B 0 0")
                    .BuildBoard());
        }

        [Theory]
        [InlineData(new []{"PMMMMMM", "PMMMMMM"}, new []{"0 3 North", "1 1 North"})]
        [InlineData(new []{"PMMMMMM", "PRMMMMM"}, new []{"0 3 North", "4 1 East"})]
        [InlineData(new []{"KMMMMM", "KMMMMM"}, new []{"0 0 NorthEast", "2 2 NorthEast"})]
        [InlineData(new []{"PRMMMLMLMMMM"}, new []{"0 1 West"})]
        [InlineData(new []{"PRMMMLMLMMMM", "PMRMMMLMMMLMMMMM"}, new []{"0 1 West", "4 4 West"})]
        [InlineData(new []{"PRMMMMLMMMMLMMMMM"}, new []{"4 4 West"})]
        [InlineData(new []{"PMXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMXRRR"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMRMLMRM", "PMXRRR"}, new []{"1 2 East", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 2 East"})]
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