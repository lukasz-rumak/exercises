using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IEventsCreator
    {
        List<Event> CreateEventsForPawn(Players player, (int x, int y) pos);
    }
}