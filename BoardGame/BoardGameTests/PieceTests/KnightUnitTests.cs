using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class KnightUnitTests
    {
        private IPiece _knight;

        public KnightUnitTests()
        {
            _knight = new Knight(0, new List<(int, int)> {(1, 2)});
        }

        [Theory]
        [InlineData(Direction.NorthEast, Direction.SouthEast, new[] {1, 2})]
        [InlineData(Direction.SouthEast, Direction.SouthWest, new[] {1, 2})]
        [InlineData(Direction.SouthWest, Direction.NorthWest, new[] {1, 2})]
        [InlineData(Direction.NorthWest, Direction.NorthEast, new[] {1, 2})]
        [InlineData(Direction.None, Direction.None, new[] {1, 2})]
        public void ReturnExpectedVersusActualForChangeDirectionToRight(Direction input, Direction expectedDirection,
            int[] expectedPossibleMoves)
        {
            _knight.Position.Direction = input;
            _knight.ChangeDirectionToRight();
            Assert.Equal(expectedDirection, _knight.Position.Direction);
            Assert.Equal(new List<(int, int)> {(expectedPossibleMoves[0], expectedPossibleMoves[1])},
                _knight.PossibleMoves);
        }

        [Theory]
        [InlineData(Direction.NorthWest, Direction.SouthWest, new[] {1, 2})]
        [InlineData(Direction.SouthWest, Direction.SouthEast, new[] {1, 2})]
        [InlineData(Direction.SouthEast, Direction.NorthEast, new[] {1, 2})]
        [InlineData(Direction.NorthEast, Direction.NorthWest, new[] {1, 2})]
        [InlineData(Direction.None, Direction.None, new[] {1, 2})]
        public void ReturnExpectedVersusActualForChangeDirectionToLeft(Direction input, Direction expectedDirection,
            int[] expectedPossibleMoves)
        {
            _knight.Position.Direction = input;
            _knight.ChangeDirectionToLeft();
            Assert.Equal(expectedDirection, _knight.Position.Direction);
            Assert.Equal(new List<(int, int)> {(expectedPossibleMoves[0], expectedPossibleMoves[1])},
                _knight.PossibleMoves);
        }
    }
}