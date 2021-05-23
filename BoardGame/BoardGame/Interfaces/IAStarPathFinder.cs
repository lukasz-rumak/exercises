using System.Collections.Generic;
using BoardGame.Managers;

namespace BoardGame.Interfaces
{
    public interface IAStarPathFinder
    {
        bool ArePathsExistWhenNewWallIsAdded(List<Wall> walls, List<IBerry> berries, int boardSize);
        bool IsPathExistsWhenNewBerryIsAdded(List<Wall> walls, int boardSize, int berryPositionX, int berryPositionY);
    }
}