﻿using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class BoardGame8x8SizeTests
    {
        private readonly IGame _game;

        public BoardGame8x8SizeTests()
        {
            _game = new GameMaster(new Validator(),
                new BoardBuilder(new EventHandler(new ConsoleOutput()), new Validator()).WithSize(8).BuildBoard(),
                new ConsoleOutput());
        }

        [Theory]
        [InlineData(new []{"PMRMLMRM"}, new []{"2 2 East"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"3 2 North"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 5 North"})]
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