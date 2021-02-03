using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IGameBoard _board;
        private readonly IPresentation _present;
        private readonly PieceFactory _pieceFactory;
        
        public GameMaster(IValidator validator, IGameBoard board, IPresentation present)
        {
            _validator = validator;
            _board = board;
            _present = present;
            _pieceFactory = new PieceFactory();
            _pieceFactory.Register("P", new PawnAbstractFactory());
            _pieceFactory.Register("K", new KnightAbstractFactory());
            _validator.AllowedPieceTypes = _pieceFactory.GetRegisteredKeys();
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};
            
            AddWallsToBoard(instructions);
            instructions = RemoveWallsFromTheInstructions(instructions);
            var pieces = CreatePieces(instructions);
            ExecuteValidation(pieces, instructions);
            ExecuteTheInstructions(pieces, instructions);
            return GetResult(pieces);
        }

        private void AddWallsToBoard(string[] instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions[0]))
                return;
            if (instructions[0][0] != 'W')
                return;
            if (!ValidateTheWallCoordinates(instructions[0]))
            {
                _present.GenerateWallCreationError();
                return;
            }

            var stringBuilder = new StringBuilder();
            foreach (var c in instructions[0].Where(c => c != ' '))
                stringBuilder.Append(c);
            var wallsToBuild = stringBuilder.ToString().Remove(0, 1).Split("W");
            foreach (var coordinates in wallsToBuild)
                CreateWall(coordinates);
        }

        private bool ValidateTheWallCoordinates(string instruction)
        {
            var counter = 0;
            foreach (var c in instruction)
            {
                if (counter == 0 && c != 'W')
                    return false;
                if ((counter == 1 || counter == 3 || counter == 5 || counter == 7) && c != ' ')
                    return false;
                if ((counter == 2 || counter == 4 || counter == 6 || counter == 8) &&
                    !int.TryParse(c.ToString(), out var r))
                    return false;
                counter += 1;
                if (counter == 9) counter = 0;
            }

            return true;
        }

        private void CreateWall(string coordinates)
        {
            _board.Walls.Add(
                new Wall
                {
                    WallPositionField1 = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                    WallPositionField2 = (int.Parse(coordinates[2].ToString()), int.Parse(coordinates[3].ToString()))
                }
            );
        }
        
        private string[] RemoveWallsFromTheInstructions(string[] instructions)
        {
            if (!string.IsNullOrWhiteSpace(instructions[0]) && instructions[0][0] == 'W')
                return instructions.Where((source, index) => index != 0).ToArray();
            return instructions;
        }
        
        private List<IPiece> CreatePieces(IReadOnlyList<string> instructions)
        {
            var pieces = new List<IPiece>();
            for (var i = 0; i < instructions.Count; i++)
            {
                if (_board.WithSize > i)
                {
                    pieces.Add(_pieceFactory.GetPiece(instructions[i].Length > 0 ? instructions[i][0].ToString() : string.Empty, i));
                    _board.Board[i, i].TakenBy = pieces[i];
                }
            }
            return pieces;
        }

        private void ExecuteValidation(IReadOnlyList<IPiece> pieces, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < pieces.Count; i++)
                if (!_validator.ValidateInput(instructions[i]))
                    pieces[i].IsAlive = false;
        }
        
        private void ExecuteTheInstructions(IReadOnlyList<IPiece> pieces, IReadOnlyList<string> instructions)
        {
            var longestInstruction = GetTheLongestInstruction(pieces, instructions);
            
            for (var i = 0; i < longestInstruction; i++)
                for (var j = 0; j < pieces.Count; j++)
                    if (pieces[j].IsAlive)
                        if (instructions[j].Length > i)
                        {
                            _board.ExecuteThePlayerInstruction(pieces[j], instructions[j][i]);
                            _present.GenerateOutput(_board, pieces);
                        }
        }

        private int GetTheLongestInstruction(IReadOnlyCollection<IPiece> pieces, IReadOnlyList<string> instructions)
        {
            var longest = 0;
            for (var i = 0; i < pieces.Count; i++)
                if (instructions[i].Length > longest)
                    longest = instructions[i].Length;
            return longest;
        }
        
        private string[] GetResult(IReadOnlyList<IPiece> pieces)
        {
            var result = new string[pieces.Count];
            for (var i = 0; i < pieces.Count; i++)
            {
                result[i] = pieces[i].IsAlive
                    ? new StringBuilder().Append(pieces[i].Position.X).Append(" ").Append(pieces[i].Position.Y)
                        .Append(" ").Append(pieces[i].Position.Direction).ToString()
                    : result[i] = @"Instruction not clear. Exiting...";
            }
            
            return result;
        }
    }
}