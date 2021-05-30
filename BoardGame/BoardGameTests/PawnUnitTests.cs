using System.Collections.Generic;
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
            _pawn = new Pawn(0, new List<(int, int)> {(1, 1)});
        }

        [Theory]
        [InlineData(Direction.North, Direction.East, new[] {1, 1})]
        [InlineData(Direction.East, Direction.South, new[] {1, 1})]
        [InlineData(Direction.South, Direction.West, new[] {1, 1})]
        [InlineData(Direction.West, Direction.North, new[] {1, 1})]
        [InlineData(Direction.None, Direction.None, new[] {1, 1})]
        public void ReturnExpectedVersusActualForChangeDirectionToRight(Direction input, Direction expectedDirection,
            int[] expectedPossibleMoves)
        {
            _pawn.Position.Direction = input;
            _pawn.ChangeDirectionToRight();
            Assert.Equal(expectedDirection, _pawn.Position.Direction);
            Assert.Equal(new List<(int, int)> {(expectedPossibleMoves[0], expectedPossibleMoves[1])},
                _pawn.PossibleMoves);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West, new[] {1, 1})]
        [InlineData(Direction.East, Direction.North, new[] {1, 1})]
        [InlineData(Direction.South, Direction.East, new[] {1, 1})]
        [InlineData(Direction.West, Direction.South, new[] {1, 1})]
        [InlineData(Direction.None, Direction.None, new[] {1, 1})]
        public void ReturnExpectedVersusActualForChangeDirectionToLeft(Direction input, Direction expectedDirection,
            int[] expectedPossibleMoves)
        {
            _pawn.Position.Direction = input;
            _pawn.ChangeDirectionToLeft();
            Assert.Equal(expectedDirection, _pawn.Position.Direction);
            Assert.Equal(new List<(int, int)> {(expectedPossibleMoves[0], expectedPossibleMoves[1])},
                _pawn.PossibleMoves);
        }
    }
}