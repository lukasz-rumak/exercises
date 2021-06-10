using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class AStarPathFinderAlgorithmUnitTests
    {
        private readonly IAStarPathFinderAlgorithm _pathFinder;

        public AStarPathFinderAlgorithmUnitTests()
        {
            _pathFinder = new AStarPathFinderAlgorithm();
        }

        [Theory]
        [InlineData(5, 0, 0, 3, 4, true)]
        [InlineData(5, 0, 0, 4, 3, true)]
        [InlineData(5, 2, 2, 4, 0, true)]
        [InlineData(5, 3, 3, 0, 4, true)]
        [InlineData(5, 0, 1, 3, 4, true)]
        [InlineData(5, 1, 0, 4, 3, true)]
        [InlineData(5, 2, 4, 3, 4, true)]
        [InlineData(5, 2, 3, 4, 3, true)]
        [InlineData(5, 0, 0, 1, 2, false)]
        [InlineData(5, 0, 1, 2, 3, false)]
        [InlineData(5, 2, 0, 4, 1, false)]
        [InlineData(5, 0, 3, 1, 2, false)]
        [InlineData(5, 4, 0, 2, 3, false)]
        [InlineData(5, 4, 3, 4, 1, false)]
        [InlineData(5, 0, 0, 4, 4, false)]
        [InlineData(5, 1, 1, 4, 4, false)]
        [InlineData(5, 2, 2, 4, 4, false)]
        [InlineData(5, 3, 3, 4, 4, false)]
        [InlineData(5, 3, 3, 5, 5, false)]
        [InlineData(10, 3, 3, 12, 12, false)]
        [InlineData(20, 3, 3, 24, 28, false)]
        public void ReturnExpectedVersusActualForAStarPathFinderAlgorithmForPawn(int boardSize, int startX, int startY,
            int targetX, int targetY, bool expectedResult)
        {
            var pawnMoves = new List<(int, int)>
            {
                (0, 1),
                (1, 0),
                (0, -1),
                (-1, 0)
            };
            var obstacles = new List<Obstacle>
            {
                new Obstacle {FromX = null, FromY = null, ToX = 1, ToY = 2},
                new Obstacle {FromX = null, FromY = null, ToX = 2, ToY = 3},
                new Obstacle {FromX = null, FromY = null, ToX = 4, ToY = 1},
                new Obstacle {FromX = 3, FromY = 4, ToX = 4, ToY = 4},
                new Obstacle {FromX = 4, FromY = 3, ToX = 4, ToY = 4},
            };
            var actual =
                _pathFinder.DoesPathExist(pawnMoves, obstacles, boardSize, startX, startY, targetX, targetY);
            Assert.Equal(expectedResult, actual);
        }
        
        [Theory]
        [InlineData(5, 0, 0, 3, 1, true)]
        [InlineData(5, 0, 0, 2, 0, true)]
        [InlineData(5, 2, 2, 4, 0, true)]
        [InlineData(5, 3, 3, 0, 4, true)]
        [InlineData(5, 1, 1, 1, 3, true)]
        [InlineData(5, 1, 1, 2, 2, true)]
        [InlineData(5, 2, 4, 3, 1, true)]
        [InlineData(5, 2, 0, 2, 2, true)]
        [InlineData(5, 0, 0, 0, 2, false)]
        [InlineData(5, 0, 0, 2, 4, false)]
        [InlineData(5, 0, 0, 4, 2, false)]
        [InlineData(5, 0, 0, 1, 2, false)]
        [InlineData(5, 0, 2, 2, 3, false)]
        [InlineData(5, 2, 0, 4, 1, false)]
        [InlineData(5, 0, 4, 1, 2, false)]
        [InlineData(5, 4, 0, 2, 3, false)]
        [InlineData(5, 4, 2, 4, 1, false)]
        [InlineData(5, 0, 0, 4, 4, false)]
        [InlineData(5, 1, 1, 4, 4, false)]
        [InlineData(5, 2, 2, 4, 4, false)]
        [InlineData(5, 3, 3, 4, 4, false)]
        [InlineData(5, 3, 3, 5, 5, false)]
        [InlineData(10, 3, 3, 12, 12, false)]
        [InlineData(20, 3, 3, 24, 28, false)]
        public void ReturnExpectedVersusActualForAStarPathFinderAlgorithmForKnight(int boardSize, int startX, int startY,
            int targetX, int targetY, bool expectedResult)
        {
            var knightMoves = new List<(int, int)>
            {
                (1, 1),
                (1, -1),
                (-1, -1),
                (-1, 1)
            };
            var obstacles = new List<Obstacle>
            {
                new Obstacle {FromX = null, FromY = null, ToX = 0, ToY = 2},
                new Obstacle {FromX = null, FromY = null, ToX = 2, ToY = 4},
                new Obstacle {FromX = null, FromY = null, ToX = 4, ToY = 2},
                new Obstacle {FromX = 3, FromY = 3, ToX = 4, ToY = 4},
            };
            var actual =
                _pathFinder.DoesPathExist(knightMoves, obstacles, boardSize, startX, startY, targetX, targetY);
            Assert.Equal(expectedResult, actual);
        }
    }
}