using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class BoardBuilderHolder : IBoardBuilderHolder
    {
        private readonly Dictionary<Guid, IBoardBuilder> _builderSessionHolder;

        public BoardBuilderHolder()
        {
            _builderSessionHolder = new Dictionary<Guid, IBoardBuilder>();
        }

        public void Add(Guid sessionId, IBoardBuilder boardBuilder)
        {
            _builderSessionHolder.Add(sessionId, boardBuilder);
        }

        public void Remove(Guid sessionId)
        {
            _builderSessionHolder.Remove(sessionId);
        }

        public IBoardBuilder Get(Guid sessionId)
        {
            return _builderSessionHolder[sessionId];
        }

        public bool IsKeyPresent(Guid sessionId)
        {
            return _builderSessionHolder.ContainsKey(sessionId);
        }
    }
}