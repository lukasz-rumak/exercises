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
            _game = new GameMaster(new Validator(), new BoardBuilder().WithSize(5).BuildBoard(), new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"PMRMLMRM", "PRMMMLMMM"}, new []{"2 2 East", "4 4 North"})]
        [InlineData(new []{"W 0 0 0 1", "PMMMMMM", "PRMMMMM"}, new []{"0 0 North", "4 1 East"})]
        [InlineData(new []{"W 0 0 0 1 W 0 0 1 1", "PMMMMMM", "PRMMMMM"}, new []{"0 0 North", "4 1 East"})]
        [InlineData(new []{"KMRMLMRM", "KRMMMLMMM"}, new []{"2 0 SouthEast", "4 2 NorthEast"})]
        [InlineData(new []{"W 0 0 1 1", "KMMMMM", "KMMMMM"}, new []{"0 0 NorthEast", "4 4 NorthEast"})]
        [InlineData(new []{"W 0 0 1 1 W 3 3 4 4", "KMMMMM", "KMMMMM"}, new []{"0 0 NorthEast", "3 3 NorthEast"})]
        [InlineData(new []{"W 1 1 2 2", "KMMMMM", "KMMMMM"}, new []{"0 0 NorthEast", "1 1 NorthEast"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"3 2 North"})]
        [InlineData(new []{"PRMMMLMM", "PMRMLMRM"}, new []{"3 2 North", "3 3 East"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 4 North"})]
        [InlineData(new []{"PMMMMM", "PRMMMLMMM"}, new []{"0 4 North", "4 4 North"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"0 4 North"})]
        [InlineData(new []{"PMMMMMMMMM", "PRMMMLMMM"}, new []{"0 4 North", "4 4 North"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"0 4 East"})]
        [InlineData(new []{"PMMMMMMMMMR", "PRMMMLMMM"}, new []{"0 4 East", "4 4 North"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"0 4 West"})]
        [InlineData(new []{"PMMMMMMMMML", "PRMMMLMMM"}, new []{"0 4 West", "4 4 North"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"4 4 East"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM", "PMMMMMMMMMRMMMMMMMM"}, new []{"3 4 East", "4 4 East"})]
        [InlineData(new []{"PRMMMMMMMMMM"}, new []{"4 0 East"})]
        [InlineData(new []{"PRMMMMMMMMMM", "PRMMMMMMMMMMM"}, new []{"4 0 East", "4 1 East"})]
        [InlineData(new []{"PRMMLMMRMMMMMMMM"}, new []{"4 2 East"})]
        [InlineData(new []{"PRMMLMMRMMMMMMMM", "PMXMMM"}, new []{"4 2 East", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PRMMLMMLMMMMMMMM"}, new []{"0 2 West"})]
        [InlineData(new []{"PRMMLMMLMMMMMMMM", "PMXMMM"}, new []{"0 2 West", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PLMMMMMMMMMM"}, new []{"0 0 West"})]
        [InlineData(new []{"PLMMMMMMMMMM", "PRMMLMMRMMMMMMMMM"}, new []{"0 0 West", "4 3 East"})]
        [InlineData(new []{"PLLMMMMMMMMMM"}, new []{"0 0 South"})]
        [InlineData(new []{"PLLMMMMMMMMMM", "PMLMMMM"}, new []{"0 0 South", "0 2 West"})]
        [InlineData(new []{"PMLMMM"}, new []{"0 1 West"})]
        [InlineData(new []{"PMLMMM", "PMLMMMM"}, new []{"0 1 West", "0 2 West"})]
        [InlineData(new []{"PRMLMLMMMMM"}, new []{"0 1 West"})]
        [InlineData(new []{"PRMLMLMMMMM", "PRMLMLMMMMMM"}, new []{"0 1 West", "0 2 West"})]
        [InlineData(new []{"PRMMMMLMMMM", "PRRLMMMMLMMMM"}, new []{"4 1 North", "4 4 North"})]
        [InlineData(new []{"PMMMMMMMMM", "PMMMMMMMMM"}, new []{"0 4 North", "1 4 North"})]
        [InlineData(new []{"PMMMM", "PRMMMM"}, new []{"0 4 North", "4 1 East"})]
        [InlineData(new []{"PM","PM","PM","PM","PM","PM","PM","PM","PM","PM","PM"}, new []{"0 1 North", "1 2 North", "2 3 North", "3 4 North", "4 4 North"})]
        [InlineData(new []{"PMXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMXRRR"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMRMLMRM", "PMXRRR"}, new []{"1 2 East", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"PMXMMM", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"0 1 0 1 W 0 1 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"X 0 1 0 1 W 0 1 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"W 0 1 X 1 W 0 1 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"W 0 1 0 1 W 0 1 1 X", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"W 0 1 0 1 W 01 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"W 0 1 01 W 0 1 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
        [InlineData(new []{"W 0 1 0 1 0 1 1 1", "PMRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
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