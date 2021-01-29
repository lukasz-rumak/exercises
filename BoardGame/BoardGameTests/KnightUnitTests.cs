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
            _knight = new Knight(0);
        }

        [Theory]
        [InlineData(Direction.NorthEast, Direction.SouthEast)]
        [InlineData(Direction.SouthEast, Direction.SouthWest)]
        [InlineData(Direction.SouthWest, Direction.NorthWest)]
        [InlineData(Direction.NorthWest, Direction.NorthEast)]
        [InlineData(Direction.None, Direction.None)]
        public void ReturnExpectedVersusActualForChangeDirectionToRight(Direction input, Direction expectedResult)
        {
            _knight.Position.Direction = input;
            _knight.ChangeDirectionToRight();
            Assert.Equal(expectedResult, _knight.Position.Direction);
        }

        [Theory]
        [InlineData(Direction.NorthWest, Direction.SouthWest)]
        [InlineData(Direction.SouthWest, Direction.SouthEast)]
        [InlineData(Direction.SouthEast, Direction.NorthEast)]
        [InlineData(Direction.NorthEast, Direction.NorthWest)]
        [InlineData(Direction.None, Direction.None)]
        public void ReturnExpectedVersusActualForChangeDirectionToLeft(Direction input, Direction expectedResult)
        {
            _knight.Position.Direction = input;
            _knight.ChangeDirectionToLeft();
            Assert.Equal(expectedResult, _knight.Position.Direction);
        }
    }
}