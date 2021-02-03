using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class WallsUnitTestsTmp
    {
        private GameBoard _board;

        public WallsUnitTestsTmp()
        {
            _board = new GameBoard {WithSize = 5};
            _board.Board = _board.GenerateBoard(5);
        }
        
//        [Theory]
//        [InlineData(Direction.East, 0, 1, false)]
//        public void ReturnExpectedVersusActualForWalls(Direction direction, int x, int y, bool expectedResult)
//        {
//            var actual = _board.WatchOutForWallsTmp(direction, x, y);
//            Assert.Equal(expectedResult, actual);
//        }
    }
}