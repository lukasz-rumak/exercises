using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGame.Models.Berries;
using Xunit;

namespace BoardGameTests
{
    public class ArePathsExistWhenNewWallIsAddedTests
    {
        private readonly IAStarPathFinder _aStarPathFinder;

        public ArePathsExistWhenNewWallIsAddedTests()
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

            var berries = new List<IBerry>
            {
                new BlueBerry
                {
                    BerryPosition = (0, 2)
                },
                new StrawBerry
                {
                    BerryPosition = (1, 3)
                },
                new BlueBerry
                {
                    BerryPosition = (3, 1)
                },
                new StrawBerry
                {
                    BerryPosition = (2, 4)
                }
            };
            
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
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
            
            var berries = new List<IBerry>
            {
                new BlueBerry
                {
                    BerryPosition = (0, 2)
                },
                new StrawBerry
                {
                    BerryPosition = (1, 3)
                },
                new BlueBerry
                {
                    BerryPosition = (3, 1)
                },
                new StrawBerry
                {
                    BerryPosition = (2, 4)
                }
            };
            
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
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
                },
                new Wall
                {
                    WallPositionField1 = (1, 1),
                    WallPositionField2 = (0, 0)
                }
            };

            var berries = new List<IBerry>
            {
                new BlueBerry
                {
                    BerryPosition = (0, 2)
                },
                new StrawBerry
                {
                    BerryPosition = (1, 3)
                },
                new BlueBerry
                {
                    BerryPosition = (3, 1)
                },
                new StrawBerry
                {
                    BerryPosition = (2, 4)
                }
            };
            
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
            Assert.False(actual);
        }
        
        [Fact]
        public void ReturnFalseForNonExistingPathNoAccess()
        {
            var walls = new List<Wall>
            {
                new Wall
                {
                    WallPositionField1 = (3, 3),
                    WallPositionField2 = (2, 4)
                },
                new Wall
                {
                    WallPositionField1 = (3, 3),
                    WallPositionField2 = (2, 2)
                },
                new Wall
                {
                    WallPositionField1 = (3, 3),
                    WallPositionField2 = (4, 2)
                }
            };
            
            var berries = new List<IBerry>
            {
                new BlueBerry
                {
                    BerryPosition = (0, 2)
                },
                new StrawBerry
                {
                    BerryPosition = (1, 3)
                },
                new BlueBerry
                {
                    BerryPosition = (3, 1)
                },
                new StrawBerry
                {
                    BerryPosition = (2, 4)
                }
            };
            
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
            Assert.False(actual);
        }
        
        [Fact]
        public void ReturnFalseForNotReachableFieldByKnight()
        {
            var walls = new List<Wall>();
            var berries = new List<IBerry>
            {
                new BlueBerry
                {
                    BerryPosition = (4, 3)
                }
            };
                
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
            Assert.False(actual);
        }
    }
}