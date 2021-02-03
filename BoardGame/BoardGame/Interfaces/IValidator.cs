using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IValidator
    {
        List<string> AllowedPieceTypes { get; set; }
        bool ValidateInstructionsInput(string input);
        bool ValidateWallsInput(string input);
    }
}