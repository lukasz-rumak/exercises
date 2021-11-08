using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IEventsHandler
    {
        List<Event> ReturnTheBestEventsToExecute(Players player);
        List<List<Event>> CreateAllEventsForGivenPlayer(Players player);
        List<List<Event>> CreateAllEventsForGivenPawn(Players player, int pawnPositionX, int pawnPositionY);
        List<Event> SelectTheBestPathForPawn(List<Event> events);
    }
}