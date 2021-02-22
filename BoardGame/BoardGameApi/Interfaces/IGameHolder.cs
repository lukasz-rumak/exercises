using System;
using System.Collections.Generic;
using BoardGame.Managers;

namespace BoardGameApi.Interfaces
{
    public class IGameHolder
    {
        public Dictionary<Guid, GameMaster> SessionsHolder { get; set; }
    }
}