using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IAStarPathFinderAlgorithm
    {
        bool IsPathPossibleUsingAStarSearchAlgorithm(List<(int, int)> possibleMoves, List<Wall> walls, int boardSize,
            int startX, int startY, int targetX, int targetY);
    }
}