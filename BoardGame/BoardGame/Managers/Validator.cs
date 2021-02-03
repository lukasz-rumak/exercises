using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Validator : IValidator
    {
        public List<string> AllowedPieceTypes { get; set; }

        public bool ValidateInstructionsInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(c => IsPieceType(c) || IsMovementType(c));
        }

        public bool ValidateWallsInput(string input)
        {
            var counter = 0;
            foreach (var c in input)
            {
                if (counter == 0 && c != 'W')
                    return false;
                if ((counter == 1 || counter == 3 || counter == 5 || counter == 7) && c != ' ')
                    return false;
                if ((counter == 2 || counter == 4 || counter == 6 || counter == 8) &&
                    !int.TryParse(c.ToString(), out var r))
                    return false;
                counter += 1;
                if (counter == 10) counter = 0;
            }

            return true;
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