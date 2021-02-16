using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

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
            if (string.IsNullOrWhiteSpace(input) || !input.StartsWith('W')) return false;
            var inputSplited = input.Split('W')[1].Split(' ');
            if (!ValidateWallAgainstLength(inputSplited)) return false;
            inputSplited = inputSplited.Skip(1).ToArray();
            var inputConverted = ValidateWallAgainstNonIntAndConvert(inputSplited);
            if (inputConverted == null) return false;
            if (!ValidateWallAgainstPositionDifference(inputConverted)) return false;
            if (!ValidateWallAgainstBoardSize(inputConverted, boardSize)) return false;

            return true;
        }

        public ValidationResult ValidateWallInputWithReason(string input, in int boardSize)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult {IsValid = false, Reason = "Input cannot be null, empty or whitespace"};
            if (!input.StartsWith('W'))
                return new ValidationResult {IsValid = false, Reason = "Input should start with 'W'"};
            var inputSplited = input.Split('W')[1].Split(' ');
            if (!ValidateWallAgainstLength(inputSplited)) return new ValidationResult {IsValid = false};
            inputSplited = inputSplited.Skip(1).ToArray();
            var inputConverted = ValidateWallAgainstNonIntAndConvert(inputSplited);
            if (inputConverted == null) return new ValidationResult {IsValid = false};
            if (!ValidateWallAgainstPositionDifference(inputConverted)) return new ValidationResult {IsValid = false};
            if (!ValidateWallAgainstBoardSize(inputConverted, boardSize)) return new ValidationResult {IsValid = false};

            return new ValidationResult {IsValid = true, Reason = ""};
        }

        private bool ValidateWallAgainstLength(IReadOnlyList<string> input)
        {
            return input.Count == 5;
        }
        
        private int[] ValidateWallAgainstNonIntAndConvert(IReadOnlyList<string> input)
        {
            var inputConverted = new int[4];
            for (int i = 0; i < input.Count; i++)
            {
                if (int.TryParse(input[i], out var result))
                    inputConverted[i] = result;
                else
                    return null;
            }

            return inputConverted;
        }

        private bool ValidateWallAgainstPositionDifference(IReadOnlyList<int> input)
        {
            if (!new[] {0, 1}.Contains(Math.Abs(input[0] - input[2]))) return false;
            if (!new[] {0, 1}.Contains(Math.Abs(input[1] - input[3]))) return false;
            return true;
        }

        private bool ValidateWallAgainstBoardSize(IReadOnlyList<int> input, int boardSize)
        {
            return input.All(t => t >= 0 && t < boardSize);
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