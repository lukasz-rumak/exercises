using System;
using BoardGame.Interfaces;

namespace BoardGameApi.Interfaces
{
    public interface IGameHolder
    {
        void Add(Guid sessionId, IGame gameMaster);
        IGame Get(Guid sessionId);
        bool IsKeyPresent(Guid sessionId);
    }
}