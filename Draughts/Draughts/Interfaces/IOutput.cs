using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IOutput
    {
        void GenerateBoardVisualization(IBoardCreator board);
        void GeneratePlayerMovement(Players player, List<Event> events);
        void GenerateSummary(IBoardCreator board);
    }
}