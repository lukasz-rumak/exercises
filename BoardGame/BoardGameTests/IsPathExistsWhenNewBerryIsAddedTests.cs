using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using Xunit;

namespace BoardGameTests
{
    public class IsPathExistsWhenNewBerryIsAddedTests
    {
        private readonly IAStarPathFinder _aStarPathFinder;

        public IsPathExistsWhenNewBerryIsAddedTests()
        {
            _aStarPathFinder = new AStarPathFinder();
        }

        [Fact]
        public void ReturnTrueForExistingPathSimple()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (2, 2)
                }
            };
            
            var actual = _aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, 5,4, 4);
            Assert.True(actual);
        }
        
        [Fact]
        public void ReturnTrueForExistingPathMoreComplex()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (1, 0),
                    WallPositionField2 = (1, 1)
                },
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (1, 0)
                }
            };
            
            var actual = _aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, 5, 4, 2);
            Assert.True(actual);
        }
        
        [Fact]
        public void ReturnFalseForNonExistingPathPieceBlocked()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (1, 0),
                    WallPositionField2 = (1, 1)
                },
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (1, 0)
                },
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (1, 1)
                }
            };
            
            var actual = _aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, 5, 4, 2);
            Assert.False(actual);
        }
        
        [Fact]
        public void ReturnFalseForNonExistingPathNoAccess()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (2, 3),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (3, 3),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (3, 2),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (3, 1),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (2, 1),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (1, 2),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (1, 3),
                    WallPositionField2 = (2, 2)
                }
            };
            
            var actual = _aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, 5, 2, 2);
            Assert.False(actual);
        }
        
        [Fact]
        public void ReturnFalseForNotReachableFieldByKnight()
        {
            var walls = new List<Wall>();
            var actual = _aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, 5, 3, 2);
            Assert.False(actual);
        }
    }
}