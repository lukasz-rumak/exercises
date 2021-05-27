using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class AStarPathFinderAdapter : IAStarPathFinderAdapter
    {
        private readonly IAStarPathFinderAlgorithm _aStarPathFinder;
        private readonly IPlayerMovement _player;
        private readonly Dictionary<string, List<(int, int)>> _possibleMoves;

        public AStarPathFinderAdapter(IAStarPathFinderAlgorithm aStarPathFinder, IPlayerMovement player)
        {
            _aStarPathFinder = aStarPathFinder;
            _player = player;
            _possibleMoves = CreatePossibleMovesDictionary();
        }

        public bool ArePathsExistWhenNewWallIsAdded(List<Wall> walls, List<IBerry> berries, int boardSize)
        {
            foreach (var possibleMove in _possibleMoves)
            {
                var counter = 0;
                foreach (var berry in berries)
                {
                    for (int i = 0; i < boardSize; i++)
                    {
                        if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(possibleMove.Value, walls,
                            boardSize, i, i, berry.BerryPosition.Item1,
                            berry.BerryPosition.Item2))
                        {
                            counter++;
                            break;
                        }
                    }
                }
                if (berries.Count != counter)
                    return false;
            }

            return true;
        }

        public bool IsPathExistsWhenNewBerryIsAdded(List<Wall> walls, int boardSize, int berryPositionX,
            int berryPositionY)
        {
            foreach (var possibleMove in _possibleMoves)
            {
                var status = false;
                for (int i = 0; i < boardSize; i++)
                {
                    if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(possibleMove.Value, walls,
                        boardSize, i, i, berryPositionX, berryPositionY))
                    {
                        status = true;
                        break;
                    }
                }
                if (!status)
                    return false;
            }

            return true;
        }

        private Dictionary<string, List<(int, int)>> CreatePossibleMovesDictionary()
        {
            return _player.GetRegisteredPieceKeys().ToDictionary(k => k, k => _player.GetPossibleMoves(k));
        }
    }
}