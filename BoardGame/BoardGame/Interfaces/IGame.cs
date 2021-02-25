using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IGame
    {
        string[] PlayTheGame(string[] instructions);
        void CreatePlayers(IReadOnlyList<string> instructions);
    }
}