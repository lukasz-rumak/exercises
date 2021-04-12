using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidatorBerry
    {
        ValidationResult ValidateBerryInputWithReason(string input, int boardSize);
    }
}