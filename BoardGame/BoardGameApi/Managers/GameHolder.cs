using System;
using System.Collections.Generic;
using BoardGame.Managers;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class GameHolder : IGameHolder
    {
        public Dictionary<Guid, GameMaster> SessionsHolder { get; set; }

        public GameHolder()
        {
            SessionsHolder = new Dictionary<Guid, GameMaster>();
        }
        
    }
}