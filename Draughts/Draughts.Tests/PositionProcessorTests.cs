using Draughts.Managers;
using Draughts.Models;
using FluentAssertions;
using Xunit;

namespace Draughts.Tests
{
    public class PositionProcessorTests
    {
        [Theory]
        [InlineData(Players.Player1, Direction.Left, 1, 1, 0, 2, -1, 3)]
        [InlineData(Players.Player1, Direction.Right, 1, 1, 2, 2, 3, 3)]
        [InlineData(Players.Player1, Direction.Left, 0, 0, -1, 1, -2, 2)]
        [InlineData(Players.Player1, Direction.Right, 2, 0, 3, 1, 4, 2)]
        [InlineData(Players.Player2, Direction.Left, 0, 4, -1, 3, -2, 2)]
        [InlineData(Players.Player2, Direction.Right, 0, 4, 1, 3, 2, 2)]
        [InlineData(Players.Player2, Direction.Left, 3, 3, 2, 2, 1, 1)]
        [InlineData(Players.Player2, Direction.Right, 3, 3, 4, 2, 5, 1)]
        public void GivenSetOfInputsParametersAndExpectedValuesWhenTheReturnPositionsMethodIsExecutedThenCorrectPositionsAreReturned(
            Players player, Direction direction, int x, int y, 
            int expectedNearbyX, int expectedNearbyY, int expectedDistantX, int expectedDistantY) 
        {
            var positionProcessor = new PositionProcessor();
            
            var result = positionProcessor.ReturnPositions(player, direction, x, y);
            
            var expected = new Positions
            {
                NearbyX = expectedNearbyX,
                NearbyY = expectedNearbyY,
                DistantX = expectedDistantX,
                DistantY = expectedDistantY
            };
            
            expected.Should().BeEquivalentTo(result);
        }
    }
}