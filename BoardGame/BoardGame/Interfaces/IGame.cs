using System;
using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGame
    {
        ObjectFactory.ObjectFactory ObjectFactory { get; set; }
        void RunBoardBuilder(IGameBoard board);
        void StartBoardBuilder(IBoardBuilder board);
        void AddWallToBoard(string wallCoordinates);
        void FinaliseBoardBuilder();
        bool IsBoardBuilt();
        string[] PlayTheGame(string[] instructions);
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