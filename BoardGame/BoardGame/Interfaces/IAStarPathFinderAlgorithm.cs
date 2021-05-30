using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IAStarPathFinderAlgorithm
    {
        bool IsPathExists(List<(int, int)> movementRules, List<Obstacle> obstacles, int boardSize,
            int startX, int startY, int targetX, int targetY);
    }
}