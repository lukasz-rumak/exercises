using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGame.Models.Berries;
using Xunit;

namespace BoardGameTests
{
    public class DoPathsExistWhenNewWallIsAddedTests
    {
        private readonly AStarPathFinderAdapter _aStarPathFinder;

        public DoPathsExistWhenNewWallIsAddedTests()
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
                    BerryPosition = (4, 0)
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
                    BerryPosition = (4, 0)
                },
                new StrawBerry
                {
                    BerryPosition = (2, 4)
                }
            };
            
            var actual = _aStarPathFinder.ArePathsExistWhenNewWallIsAdded(walls, berries, 5);
            Assert.False(actual);
        }
    }
}