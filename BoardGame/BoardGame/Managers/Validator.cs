using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Validator : IValidator
    {
        public List<string> AllowedPieceTypes { get; set; }

        public bool ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(c => IsPieceType(c) || IsMovementType(c));
        }

        private bool IsPieceType(char c)
        {
            return AllowedPieceTypes.Contains(c.ToString());
        }

        private bool IsMovementType(char c)
        {
            return c == 'M' || c == 'R' || c == 'L';
        }
    }
}