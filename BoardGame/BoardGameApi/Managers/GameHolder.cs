using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class GameHolder : IGameHolder
    {
        private readonly Dictionary<Guid, IGameApi> _sessionsHolder;

        public GameHolder()
        {
            _sessionsHolder = new Dictionary<Guid, IGameApi>();
        }

        public void Add(Guid sessionId, IGameApi gameMaster)
        {
            _sessionsHolder.Add(sessionId, gameMaster);
        }

        public IGameApi Get(Guid sessionId)
        {
            return _sessionsHolder[sessionId];
        }

        public bool IsKeyPresent(Guid sessionId)
        {
            return _sessionsHolder.ContainsKey(sessionId);
        }
    }
}