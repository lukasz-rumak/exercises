using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class GameHolder : IGameHolder
    {
        private readonly Dictionary<Guid, IGame> _sessionsHolder;

        public GameHolder()
        {
            _sessionsHolder = new Dictionary<Guid, IGame>();
        }

        public void Add(Guid sessionId, IGame gameMaster)
        {
            _sessionsHolder.Add(sessionId, gameMaster);
        }

        public IGame Get(Guid sessionId)
        {
            return _sessionsHolder[sessionId];
        }

        public bool IsKeyPresent(Guid sessionId)
        {
            return _sessionsHolder.ContainsKey(sessionId);
        }
    }
}