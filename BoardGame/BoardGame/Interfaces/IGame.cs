using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGame
    {
        GameStatus GameStatus { get; set; }
        string[] PlayTheGame(string[] instructions);
        void CreatePlayers(IReadOnlyList<string> instructions);
        void MovePlayer(IReadOnlyList<string> instructions, int playerId);
        void MovePlayers(IReadOnlyList<string> instructions);
        string GenerateOutputApi();
    }
}