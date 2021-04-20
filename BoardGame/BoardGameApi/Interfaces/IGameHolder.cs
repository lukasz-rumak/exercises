using System;
using BoardGame.Interfaces;

namespace BoardGameApi.Interfaces
{
    public interface IGameHolder
    {
        void Add(Guid sessionId, IGameApi gameMaster);
        IGameApi Get(Guid sessionId);
        bool IsKeyPresent(Guid sessionId);
    }
}