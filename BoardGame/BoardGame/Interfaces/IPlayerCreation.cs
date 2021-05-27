using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IPlayerCreation
    {
        List<string> GetRegisteredPieceKeys();
        void CreatePlayers(IGameBoard board, IReadOnlyList<string> instructions);
        int ReturnPlayersNumber();
        IReadOnlyList<IPiece> ReturnPlayersInfo();
        IPiece ReturnPlayerInfo(int playerId);
    }
}