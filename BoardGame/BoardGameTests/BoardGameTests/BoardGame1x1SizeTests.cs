using System;
using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;
using EventHandler = BoardGame.Managers.EventHandler;

namespace BoardGameTests
{
    public class BoardGame1x1SizeTests
    {
        private readonly IGame _game;

        public BoardGame1x1SizeTests()
        {
            _game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(),
                new Validator(), new Validator(), new PlayersHandler(), new BerryCreator(),
                new AStarPathFinderAdapter(new AStarPathFinderAlgorithm(), new PlayersHandler()));
            _game.RunBoardBuilder(new BoardBuilder(_game.ObjectFactory.Get<IEventHandler>(),
                    _game.ObjectFactory.Get<IValidatorWall>(), _game.ObjectFactory.Get<IValidatorBerry>(),
                    _game.ObjectFactory.Get<IBerryCreator>(), _game.ObjectFactory.Get<IAStarPathFinderAdapter>())
                .WithSize(1).BuildBoard());
        }

        [Theory]
        [InlineData(new[] {"PMRMLMRM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PRMMMLMM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMMMMM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMMMMMMMMM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMMMMMMMMMR"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMMMMMMMMML"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMMMMMMMMMRMMMMMMM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {"PMXMMM"}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {""}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(new[] {" "}, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        [InlineData(null, new[] {"Please create board first!", "Cannot create board with size less than 3"})]
        public void ReturnExpectedVersusActualForDifferentInputInstructions(string[] input, string[] expectedResult)
        {
            var actual = _game.PlayTheGame(input);
            Assert.Equal(expectedResult, actual);
        }
    }
}