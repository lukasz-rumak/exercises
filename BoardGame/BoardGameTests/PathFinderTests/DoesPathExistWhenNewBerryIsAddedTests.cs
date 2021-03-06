using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using Xunit;

namespace BoardGameTests
{
    public class DoesPathExistWhenNewBerryIsAddedTests
    {
        private readonly IAStarPathFinderAdapter _aStarPathFinder;

        public DoesPathExistWhenNewBerryIsAddedTests()
        {
            _aStarPathFinder = new AStarPathFinderAdapter(new AStarPathFinderAlgorithm(), new PlayersHandler());
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
            
            var actual = _aStarPathFinder.DoesPathExistWhenNewBerryIsAdded(walls, 5,4, 4);
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
            
            var actual = _aStarPathFinder.DoesPathExistWhenNewBerryIsAdded(walls, 5, 4, 2);
            Assert.True(actual);
        }
        
        [Fact]
        public void ReturnTrueForExistingPathWhenFirstPieceBlocked()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (1, 1)
                },
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (0, 0)
                },
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (0, 1)
                },
                new Wall
                {
                    WallPositionField1 = (0, 1),
                    WallPositionField2 = (0, 0)
                },
                new Wall
                {
                    WallPositionField1 = (0, 0),
                    WallPositionField2 = (1, 0)
                },
                new Wall
                {
                    WallPositionField1 = (1, 0),
                    WallPositionField2 = (0, 0)
                }
            };
            
            var actual = _aStarPathFinder.DoesPathExistWhenNewBerryIsAdded(walls, 5, 4, 2);
            Assert.True(actual);
        }
        
        [Fact]
        public void ReturnFalseForNonExistingPathWhenNoAccessForKnightAndPawn()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (4, 0),
                    WallPositionField2 = (3, 0)
                },
                new Wall
                {
                    WallPositionField1 = (3, 0),
                    WallPositionField2 = (4, 0)
                },
                new Wall
                {
                    WallPositionField1 = (4, 0),
                    WallPositionField2 = (3, 1)
                },
                new Wall
                {
                    WallPositionField1 = (3, 1),
                    WallPositionField2 = (4, 0)
                },
                new Wall
                {
                    WallPositionField1 = (4, 0),
                    WallPositionField2 = (4, 1)
                },
                new Wall
                {
                    WallPositionField1 = (4, 1),
                    WallPositionField2 = (4, 0)
                }
            };
            
            var actual = _aStarPathFinder.DoesPathExistWhenNewBerryIsAdded(walls, 5, 4, 0);
            Assert.False(actual);
        }
        
        [Fact]
        public void ReturnFalseForNonExistingPathWhenNoAccessForPawn()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (4, 0),
                    WallPositionField2 = (3, 0)
                },
                new Wall
                {
                    WallPositionField1 = (3, 0),
                    WallPositionField2 = (4, 0)
                },
                new Wall
                {
                    WallPositionField1 = (4, 0),
                    WallPositionField2 = (4, 1)
                },
                new Wall
                {
                    WallPositionField1 = (4, 1),
                    WallPositionField2 = (4, 0)
                }
            };
            
            var actual = _aStarPathFinder.DoesPathExistWhenNewBerryIsAdded(walls, 5, 4, 0);
            Assert.False(actual);
        }
    }
}