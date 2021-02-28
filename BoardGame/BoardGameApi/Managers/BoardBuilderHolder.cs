using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGameApi.Interfaces;

namespace BoardGameApi.Managers
{
    public class BoardBuilderHolder : IBoardBuilderHolder
    {
        public Dictionary<Guid, IBoardBuilder> BuilderSessionHolder { get; set; }

        public BoardBuilderHolder()
        {
            BuilderSessionHolder = new Dictionary<Guid, IBoardBuilder>();
        }
    }
}