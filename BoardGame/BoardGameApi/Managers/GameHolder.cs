using System;
using System.Collections.Generic;
using BoardGame.Managers;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class GameHolder : IGameHolder
    {
        private readonly Dictionary<Guid, GameMaster> _sessionsHolder;

        public GameHolder()
        {
            _sessionsHolder = new Dictionary<Guid, GameMaster>();
        }

        public void Add(Guid sessionId, GameMaster gameMaster)
        {
            _sessionsHolder.Add(sessionId, gameMaster);
        }

        public GameMaster Get(Guid sessionId)
        {
            return _sessionsHolder[sessionId];
        }

        public bool IsKeyPresent(Guid sessionId)
        {
            return _sessionsHolder.ContainsKey(sessionId);
        }
    }
}