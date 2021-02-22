using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidatorWall
    {
        ValidationResult ValidateWallInputWithReason(string input, int boardSize);
    }
}