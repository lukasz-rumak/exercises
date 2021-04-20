using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IGame
    {
        ObjectFactory.ObjectFactory ObjectFactory { get; set; }
        void RunBoardBuilder(IGameBoard board);
        string[] PlayTheGame(string[] instructions);
        void CreatePlayers(IReadOnlyList<string> instructions);
    }
}