using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Validator : IValidator
    {
        private readonly List<string> _allowedPieceTypes;

        public Validator(List<string> allowedPieceTypes)
        {
            _allowedPieceTypes = allowedPieceTypes;
        }
        
        public bool ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(c => IsPieceType(c) || IsMovementType(c));
        }

        private bool IsPieceType(char c)
        {
            return c == 'P' || c == 'K';
        }

        private bool IsMovementType(char c)
        {
            return c == 'M' || c == 'R' || c == 'L';
        }
    }
}