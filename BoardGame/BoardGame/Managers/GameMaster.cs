using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IGameBoard _board;
        private readonly IPlayer _player;
        private readonly IPresentation _presentation;
        private readonly PieceFactory _pieceFactory;
        private readonly List<string> _gameResult;
        
        public GameMaster(IValidator validator, IGameBoard board, IPlayer player, IPresentation presentation)
        {
            _validator = validator;
            _board = board;
            _player = player;
            _presentation = presentation;
            _pieceFactory = new PieceFactory();
            _pieceFactory.Register("P", new PawnAbstractFactory());
            _pieceFactory.Register("K", new KnightAbstractFactory());
            _validator.AllowedPieceTypes = _pieceFactory.GetRegisteredKeys();
            _gameResult = new List<string>();
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};

            CreatePlayers(instructions);
            ExecuteValidation(_player.Players, instructions);
            ExecuteTheInstructions(_player.Players, instructions);
            CollectResult(_player.Players);
            return _gameResult.ToArray();
        }

        public void CreatePlayers(IReadOnlyList<string> instructions)
        {
            _player.CreatePlayers(_board, _pieceFactory, instructions);
        }
        
        private void ExecuteValidation(IReadOnlyList<IPiece> pieces, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < pieces.Count; i++)
                if (!_validator.ValidateInstructionsInput(instructions[i]))
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
                            _presentation.GenerateOutput(_board, pieces);
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
        
        private void CollectResult(IEnumerable<IPiece> pieces)
        {
            foreach (var piece in pieces)
            {
                _gameResult.Add(piece.IsAlive
                    ? new StringBuilder().Append(piece.Position.X).Append(" ").Append(piece.Position.Y)
                        .Append(" ").Append(piece.Position.Direction).ToString()
                    : @"Instruction not clear. Exiting...");
            }
        }
    }
}