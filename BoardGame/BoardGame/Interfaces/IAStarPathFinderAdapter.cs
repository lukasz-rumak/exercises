using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IAStarPathFinderAdapter
    {
        bool ArePathsExistWhenNewWallIsAdded(List<Wall> walls, List<IBerry> berries, int boardSize);
        bool IsPathExistsWhenNewBerryIsAdded(List<Wall> walls, int boardSize, int berryPositionX, int berryPositionY);
    }
}