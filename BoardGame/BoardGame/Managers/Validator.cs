using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Validator : IValidator, IValidatorWall, IValidatorBerry
    {
        public List<string> AllowedPieceTypes { get; set; }

        public bool ValidateInstructionsInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(c => IsPieceType(c) || IsMovementType(c));
        }

        public ValidationResult ValidateWallInputWithReason(string input, int boardSize, List<Wall> walls,
            List<IBerry> berries, IAStarPathFinderAlgorithm aStarPathFinder)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult {IsValid = false, Reason = "Input cannot be null, empty or whitespace"};
            if (!input.StartsWith('W'))
                return new ValidationResult {IsValid = false, Reason = "Input should start with 'W'"};
            var inputSplited = input.Split('W')[1].Split(' ');
            if (!ValidateWallAgainstLength(inputSplited))
                return new ValidationResult
                    {IsValid = false, Reason = "Input wall position should contain four chars divided by whitespace"};
            inputSplited = inputSplited.Skip(1).ToArray();
            var inputConverted = ValidateWallAgainstNonIntAndConvert(inputSplited);
            if (inputConverted == null)
                return new ValidationResult {IsValid = false, Reason = "Input wall position should be integers"};
            if (!ValidateWallAgainstPositionDifference(inputConverted))
                return new ValidationResult
                    {IsValid = false, Reason = "Input wall position difference should be 0 or 1"};
            if (!ValidateAgainstBoardSize(inputConverted, boardSize))
                return new ValidationResult
                    {IsValid = false, Reason = "Input wall position should fit into the board size"};
            if (!ValidateWallAgainstEndingGame(inputConverted, boardSize, walls, berries, aStarPathFinder))
                return new ValidationResult
                    {IsValid = false, Reason = "Input wall position should allow ending the game"};

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

        private bool ValidateAgainstBoardSize(IReadOnlyList<int> input, int boardSize)
        {
            return input.All(t => t >= 0 && t < boardSize);
        }

        private bool ValidateWallAgainstEndingGame(int[] inputConverted, int boardSize, List<Wall> walls,
            List<IBerry> berries, IAStarPathFinderAlgorithm aStarPathFinder)
        {
            var wallsNew = walls.Select(w => w).ToList();
            var wall = new Wall
            {
                WallPositionField1 = (inputConverted[0], inputConverted[1]),
                WallPositionField2 = (inputConverted[2], inputConverted[3])
            };
            wallsNew.Add(wall);
            wallsNew.Add(wall.ReversedWall());

            return aStarPathFinder.ArePathsExistWhenNewWallIsAdded(wallsNew, berries, boardSize);
        }

        public ValidationResult ValidateBerryInputWithReason(string input, int boardSize, List<Wall> walls, IAStarPathFinderAlgorithm aStarPathFinder)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult {IsValid = false, Reason = "Input cannot be null, empty or whitespace"};
            if (!input.StartsWith('B') && !input.StartsWith('S'))
                return new ValidationResult {IsValid = false, Reason = "Input should start with 'B' or 'S'"};
            if (!ValidateBerryAgainstSyntax(input))
                return new ValidationResult {IsValid = false, Reason = "Input should have the following syntax: 'B 1 2' or 'S 1 2'"};
            var position = CreateBerryIntegerList(input);
            if (!ValidateBerryAgainstStartingPosition(position))
                return new ValidationResult {IsValid = false, Reason = "Input cannot be player starting position, for example: 'B 0 0'"};
            if (!ValidateAgainstBoardSize(position, boardSize)) 
                return new ValidationResult {IsValid = false, Reason = "Input berry position should fit into the board size"};
            if (!ValidateBerryAgainstEndingGame(aStarPathFinder, walls, boardSize, position[0], position[1]))
                return new ValidationResult {IsValid = false, Reason = "Input berry position should allow ending the game"};

            return new ValidationResult {IsValid = true, Reason = ""};
        }

        private bool ValidateBerryAgainstSyntax(string input)
        {
            var regex = new Regex(@"^[BS] \d* \d*$");
            return regex.IsMatch(input ?? string.Empty);
        }

        private bool ValidateBerryAgainstStartingPosition(IReadOnlyList<int> list)
        {
            return list[0] - list[1] != 0;
        }

        private bool ValidateBerryAgainstEndingGame(IAStarPathFinderAlgorithm aStarPathFinder, List<Wall> walls, int boardSize,
            int berryPositionX, int berryPositionY)
        {
            return aStarPathFinder.IsPathExistsWhenNewBerryIsAdded(walls, boardSize, berryPositionX, berryPositionY);
        }

        private List<int> CreateBerryIntegerList(string input)
        {
            var list = new List<int>();
            var values = input[2..].Split(' ');
            foreach (var v in values)
                if (int.TryParse(v, out int result))
                    list.Add(result);
            return list;
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