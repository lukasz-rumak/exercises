using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Player : IPlayer
    {
        public List<IPiece> Players { get; set; }

        public Player()
        {
            Players = new List<IPiece>();
        }
        
        public void CreatePlayers(IGameBoard board, PieceFactory pieceFactory, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                if (board.WithSize > i)
                {
                    Players.Add(pieceFactory.GetPiece(instructions[i].Length > 0 ? instructions[i][0].ToString() : string.Empty, i));
                    board.MarkFieldAsTakenByNewPiece(Players[i]);
                }
            }
        }
    }
}