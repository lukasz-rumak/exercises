using System;
using System.Collections.Generic;
using System.Globalization;
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

        public bool ValidateWallsInput(string input, int boardSize)
        {
            if (input[0] != 'W')
                return false;
            var afterSplits = input.Split('W', StringSplitOptions.RemoveEmptyEntries)[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (afterSplits.Length != 4)
                return false;
            var afterSplitsInts = new int[4];
            for (int i = 0; i < afterSplits.Length; i++)
            {
                if (int.TryParse(afterSplits[i], out var result))
                    afterSplitsInts[i] = result;
                else
                    return false;
            }

            if (!new[] {0, 1}.Contains(Math.Abs(afterSplitsInts[0] - afterSplitsInts[2])))
                return false;
            if (!new[] {0, 1}.Contains(Math.Abs(afterSplitsInts[1] - afterSplitsInts[3])))
                return false;
            return afterSplitsInts.All(t => t >= 0 && t < boardSize);
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