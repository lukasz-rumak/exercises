using System.Collections.Generic;
using BoardGame.Managers;

namespace BoardGame.Interfaces
{
    public interface IPlayer
    {
        void CreatePlayers(IGameBoard board, PieceFactory pieceFactory, IReadOnlyList<string> instructions);
        int ReturnPlayersNumber();
        IReadOnlyList<IPiece> ReturnPlayersInfo();
        IPiece ReturnPlayerInfo(int playerId);
    }
}