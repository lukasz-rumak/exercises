using System;
using System.Collections.Generic;
using BoardGame.Managers;
using BoardGameApi.Models;

namespace BoardGameApi.Interfaces
{
    public interface IGameHolder
    {
        void Add(Guid sessionId, GameMaster gameMaster);
        GameMaster Get(Guid sessionId);
        bool IsKeyPresent(Guid sessionId);
    }
}