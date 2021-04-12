﻿using BoardGame.Interfaces;
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
        [InlineData(new []{"PMRMLMRM"}, new []{"0 0 East"})]
        [InlineData(new []{"PRMMMLMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMMMMMM"}, new []{"0 0 North"})]
        [InlineData(new []{"PMMMMMMMMMR"}, new []{"0 0 East"})]
        [InlineData(new []{"PMMMMMMMMML"}, new []{"0 0 West"})]
        [InlineData(new []{"PMMMMMMMMMRMMMMMMM"}, new []{"0 0 East"})]
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