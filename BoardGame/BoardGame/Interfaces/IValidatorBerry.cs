using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidatorBerry
    {
        ValidationResult ValidateBerryInputWithReason(string input, int boardSize, List<Wall> walls);
    }
}