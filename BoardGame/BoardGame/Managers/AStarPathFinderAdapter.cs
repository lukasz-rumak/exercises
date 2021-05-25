using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class AStarPathFinderAdapter : IAStarPathFinderAdapter
    {
        private readonly IAStarPathFinderAlgorithm _aStarPathFinder;
        public AStarPathFinderAdapter(IAStarPathFinderAlgorithm aStarPathFinder)
        {
            _aStarPathFinder = aStarPathFinder;
        }
        
        public bool ArePathsExistWhenNewWallIsAdded(List<Wall> walls, List<IBerry> berries, int boardSize)
        {
            var counterKnight = 0;
            var counterPawn = 0;

            foreach (var berry in berries)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(Piece.Knight, walls, boardSize, i, i,
                        berry.BerryPosition.Item1,
                        berry.BerryPosition.Item2))
                    {
                        counterKnight++;
                        break;
                    }
                }
            }

            if (berries.Count != counterKnight)
                return false;
            
            foreach (var berry in berries)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(Piece.Pawn, walls, boardSize, i, i,
                        berry.BerryPosition.Item1,
                        berry.BerryPosition.Item2))
                    {
                        counterPawn++;
                        break;
                    }
                }
            }

            return berries.Count == counterPawn;
        }

        public bool IsPathExistsWhenNewBerryIsAdded(List<Wall> walls, int boardSize, int berryPositionX,
            int berryPositionY)
        {
            var statusKnight = false;
            var statusPawn = false;

            for (int i = 0; i < boardSize; i++)
            {
                if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(Piece.Knight, walls, boardSize, i, i, berryPositionX,
                    berryPositionY))
                {
                    statusKnight = true;
                    break;
                }
            }

            for (int i = 0; i < boardSize; i++)
            {
                if (_aStarPathFinder.IsPathPossibleUsingAStarSearchAlgorithm(Piece.Pawn, walls, boardSize, i, i, berryPositionX,
                    berryPositionY))
                {
                    statusPawn = true;
                    break;
                }
            }

            return statusKnight && statusPawn;
        }
    }
}