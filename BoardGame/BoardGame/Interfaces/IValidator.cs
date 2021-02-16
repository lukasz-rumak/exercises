using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IValidator
    {
        List<string> AllowedPieceTypes { get; set; }
        bool ValidateInstructionsInput(string input);
        bool ValidateWallsInput(string input, int sizeBoard);
        ValidationResult ValidateWallInputWithReason(string input, in int boardSize);
    }
}