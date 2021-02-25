using System;
using System.Collections.Generic;
using BoardGame.Managers;

namespace BoardGameApi.Interfaces
{
    public interface IGameHolder
    {
        Dictionary<Guid, GameMaster> SessionsHolder { get; set; }
    }
}