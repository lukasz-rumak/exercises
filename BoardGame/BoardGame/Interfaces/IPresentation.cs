using System;
using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPresentation
    {
        void GenerateOutput(IGameBoard board, IReadOnlyList<IPiece> pieces);
        Dictionary<EventType, Action<string>> EventsOutput { get; set; }
    }
}