using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Player : IPlayer
    {
        private readonly List<IPiece> _players;
    
        public Player()
        {
            _players = new List<IPiece>();
        }
        
        public void CreatePlayers(IGameBoard board, PieceFactory pieceFactory, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                var counter = ReturnPlayersNumber();
                if (board.WithSize > i)
                {
                    _players.Add(pieceFactory.GetPiece(instructions[i].Length > 0 ? instructions[i][0].ToString() : string.Empty, counter));
                    board.MarkFieldAsTakenByNewPiece(_players[counter]);
                }
            }
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