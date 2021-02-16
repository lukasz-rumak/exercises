using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidator
    {
        List<string> AllowedPieceTypes { get; set; }
        bool ValidateInstructionsInput(string input);
        ValidationResult ValidateWallInputWithReason(string input, int boardSize);
    }
}