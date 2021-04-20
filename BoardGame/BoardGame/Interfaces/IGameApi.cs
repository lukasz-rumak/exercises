using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGameApi
    {
        ObjectFactory.ObjectFactory ObjectFactory { get; set; }
        void StartBoardBuilder(IBoardBuilder board);
        void AddWallToBoard(string wallCoordinates);
        void AddBerryToBoard(string berryCoordinates);
        void FinaliseBoardBuilder();
        bool IsBoardBuilt();
        void CreatePlayers(IReadOnlyList<string> instructions);
        void MovePlayer(IReadOnlyList<string> instructions, int playerId);
        void MovePlayers(IReadOnlyList<string> instructions);
        IPiece GetPlayerInfo(int playerId);
        EventLog GetLastEvent();
        List<EventLog> GetAllEvents();
        string GenerateOutputApi();
        bool IsGameComplete();
        void MarkGameAsComplete();
    }
}