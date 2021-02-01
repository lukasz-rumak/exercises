using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IValidator
    {
        List<string> AllowedPieceTypes { get; set; }
        bool ValidateInput(string input);
    }
}