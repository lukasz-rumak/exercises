using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class PawnUnitTests
    {
        private IPiece _pawn;
        
        public PawnUnitTests()
        {
            _pawn = new Pawn(0);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        [InlineData(Direction.None, Direction.None)]
        public void ReturnExpectedVersusActualForChangeDirectionToRight(Direction input, Direction expectedResult)
        {
            _pawn.Position.Direction = input;
            _pawn.ChangeDirectionToRight();
            Assert.Equal(expectedResult, _pawn.Position.Direction);
        }
        
        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.East, Direction.North)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.None, Direction.None)]
        public void ReturnExpectedVersusActualForChangeDirectionToLeft(Direction input, Direction expectedResult)
        {
            _pawn.Position.Direction = input;
            _pawn.ChangeDirectionToLeft();
            Assert.Equal(expectedResult, _pawn.Position.Direction);
        }
    }
}