using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGameApi.Interfaces
{
    public interface IBoardBuilderHolder
    {
        Dictionary<Guid, IBoardBuilder> BuilderSessionHolder { get; set; }
    }
}