using System.ComponentModel;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IGameHandler
    {
        void PlayTheGame(GameMode gameMode);
    }
}