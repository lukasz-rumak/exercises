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
        [InlineData(new []{"MRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"MRMLMRM", "RMMMLMMM"}, new []{"2 2 East", "4 4 North"})]
        [InlineData(new []{"RMMMLMM"}, new []{"3 2 North"})]
        [InlineData(new []{"RMMMLMM", "MRMLMRM"}, new []{"3 2 North", "3 3 East"})]
        [InlineData(new []{"MMMMM"}, new []{"0 4 North"})]
        [InlineData(new []{"MMMMM", "RMMMLMMM"}, new []{"0 4 North", "4 4 North"})]
        [InlineData(new []{"MMMMMMMMM"}, new []{"0 4 North"})]
        [InlineData(new []{"MMMMMMMMM", "RMMMLMMM"}, new []{"0 4 North", "4 4 North"})]
        [InlineData(new []{"MMMMMMMMMR"}, new []{"0 4 East"})]
        [InlineData(new []{"MMMMMMMMMR", "RMMMLMMM"}, new []{"0 4 East", "4 4 North"})]
        [InlineData(new []{"MMMMMMMMML"}, new []{"0 4 West"})]
        [InlineData(new []{"MMMMMMMMML", "RMMMLMMM"}, new []{"0 4 West", "4 4 North"})]
        [InlineData(new []{"MMMMMMMMMRMMMMMMM"}, new []{"4 4 East"})]
        [InlineData(new []{"MMMMMMMMMRMMMMMMM", "MMMMMMMMMRMMMMMMMM"}, new []{"3 4 East", "4 4 East"})]
        [InlineData(new []{"RMMMMMMMMMM"}, new []{"4 0 East"})]
        [InlineData(new []{"RMMMMMMMMMM", "RMMMMMMMMMMM"}, new []{"4 0 East", "4 1 East"})]
        [InlineData(new []{"RMMLMMRMMMMMMMM"}, new []{"4 2 East"})]
        [InlineData(new []{"RMMLMMRMMMMMMMM", "MXMMM"}, new []{"4 2 East", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"RMMLMMLMMMMMMMM"}, new []{"0 2 West"})]
        [InlineData(new []{"RMMLMMLMMMMMMMM", "MXMMM"}, new []{"0 2 West", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"LMMMMMMMMMM"}, new []{"0 0 West"})]
        [InlineData(new []{"LMMMMMMMMMM", "RMMLMMRMMMMMMMMM"}, new []{"0 0 West", "4 3 East"})]
        [InlineData(new []{"LLMMMMMMMMMM"}, new []{"0 0 South"})]
        [InlineData(new []{"LLMMMMMMMMMM", "MLMMMM"}, new []{"0 0 South", "0 2 West"})]
        [InlineData(new []{"MLMMM"}, new []{"0 1 West"})]
        [InlineData(new []{"MLMMM", "MLMMMM"}, new []{"0 1 West", "0 2 West"})]
        [InlineData(new []{"RMLMLMMMMM"}, new []{"0 1 West"})]
        [InlineData(new []{"RMLMLMMMMM", "RMLMLMMMMMM"}, new []{"0 1 West", "0 2 West"})]
        [InlineData(new []{"RMMMMLMMMM", "RRLMMMMLMMMM"}, new []{"4 1 North", "4 4 North"})]
        [InlineData(new []{"MMMMMMMMM", "MMMMMMMMM"}, new []{"0 4 North", "1 4 North"})]
        [InlineData(new []{"MMMM", "RMMMM"}, new []{"0 4 North", "4 1 East"})]
        [InlineData(new []{"MXMMM"}, new []{"Instruction not clear. Exiting..."})]
        [InlineData(new []{"MXMMM", "MXRRR"}, new []{"Instruction not clear. Exiting...", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"MRMLMRM", "MXRRR"}, new []{"1 2 East", "Instruction not clear. Exiting..."})]
        [InlineData(new []{"MXMMM", "MRMLMRM"}, new []{"Instruction not clear. Exiting...", "3 3 East"})]
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