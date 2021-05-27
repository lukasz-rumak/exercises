using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IAStarPathFinderAlgorithm
    {
        bool IsPathPossibleUsingAStarSearchAlgorithm(List<(int, int)> possibleMoves, List<Wall> walls, int boardSize,
            int piecePositionX, int piecePositionY,
            int berryPositionX, int berryPositionY);
    }
}