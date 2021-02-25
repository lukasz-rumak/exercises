using System.Collections.Generic;
using BoardGame.Managers;

namespace BoardGame.Interfaces
{
    public interface IPlayer
    {
        List<IPiece> Players { get; set; }
        void CreatePlayers(IGameBoard board, PieceFactory pieceFactory, IReadOnlyList<string> instructions);
    }
}