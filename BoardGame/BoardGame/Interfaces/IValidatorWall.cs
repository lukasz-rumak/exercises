using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidatorWall
    {
        ValidationResult ValidateWallInputWithReason(string input, int boardSize, List<Wall> walls, List<IBerry> berries, IAStarPathFinder aStarPathFinders);
    }
}