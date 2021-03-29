using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGameApi.Interfaces
{
    public interface IBoardBuilderHolder
    {
        void Add(Guid sessionId, IBoardBuilder boardBuilder);
        void Remove(Guid sessionId);
        IBoardBuilder Get(Guid sessionId);
        bool IsKeyPresent(Guid sessionId);
    }
}