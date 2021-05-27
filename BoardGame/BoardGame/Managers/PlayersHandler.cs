using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.PieceFactory;

namespace BoardGame.Managers
{
    public class PlayersHandler : IPlayerCreation, IPlayerMovement
    {
        private readonly List<IPiece> _players;
        private readonly PieceFactory.PieceFactory _pieceFactory;
    
        public PlayersHandler()
        {
            _players = new List<IPiece>();
            _pieceFactory = new PieceFactory.PieceFactory();
            _pieceFactory.Register("K", new KnightAbstractFactory());
            _pieceFactory.Register("P", new PawnAbstractFactory());
        }
        
        public List<string> GetRegisteredPieceKeys()
        {
            return _pieceFactory.GetRegisteredKeys();
        }
        
        public void CreatePlayers(IGameBoard board, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                var counter = ReturnPlayersNumber();
                if (board.WithSize > i)
                {
                    _players.Add(_pieceFactory.GetPiece(instructions[i].Length > 0 ? instructions[i][0].ToString() : string.Empty, counter));
                    board.MarkFieldAsTakenByNewPiece(_players[counter]);
                }
            }
        }
        
        public List<(int, int)> GetPossibleMoves(string description)
        {
            return _pieceFactory.GetPossibleMoves(description);
        }

        public int ReturnPlayersNumber()
        {
            return _players.Count;
        }

        public IReadOnlyList<IPiece> ReturnPlayersInfo()
        {
            return _players;
        }

        public IPiece ReturnPlayerInfo(int playerId)
        {
            return ReturnPlayersNumber() - 1 >= playerId ? _players[playerId] : null;
        }
    }
}