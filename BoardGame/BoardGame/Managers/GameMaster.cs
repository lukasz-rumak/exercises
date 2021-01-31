using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IGameBoard _board;
        private readonly IPresentation _present;
        private readonly PieceFactoryTmp _pawnFactory;
        private readonly PieceFactoryTmp _knightFactory;
        
        public GameMaster(IValidator validator, IGameBoard board, IPresentation present, PieceFactoryTmp pawnFactory, PieceFactoryTmp knightFactory)
        {
            _validator = validator;
            _board = board;
            _present = present;
            _pawnFactory = pawnFactory;
            _knightFactory = knightFactory;
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};
            
            var pieces = CreatePieces(instructions);
            ExecuteValidation(pieces, instructions);
            ExecuteTheInstructions(pieces, instructions);
            return GetResult(pieces);
        }
        
        private List<IPiece> CreatePieces(IReadOnlyList<string> instructions)
        {
            var pieces = new List<IPiece>();
            var err = 9999;
            for (var i = 0; i < instructions.Count; i++)
            {
                if (_board.WithSize > i)
                {
                    if (!string.IsNullOrWhiteSpace(instructions[i]) && instructions[i][0] == 'P')
                    {
                        pieces.Add(_pawnFactory.CreatePiece(i));
                        _board.Board[i, i].TakenBy = pieces[i];
                    }
                    else
                    {
                        pieces.Add(_pawnFactory.CreatePiece(err));
                        err -= 1;
                    }
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