using System;
using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGame
    {
        ObjectFactory.ObjectFactory ObjectFactory { get; set; }
        GameStatus GameStatus { get; set; }
        void RunBoardBuilder(IGameBoard gameBoard);
        string[] PlayTheGame(string[] instructions);
        void CreatePlayers(IReadOnlyList<string> instructions);
        void MovePlayer(IReadOnlyList<string> instructions, int playerId);
        void MovePlayers(IReadOnlyList<string> instructions);
        EventLog GetLastEvent();
        List<EventLog> GetAllEvents();
        string GenerateOutputApi();
    }
}