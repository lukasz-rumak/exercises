using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IHumanPlayer
    {
        List<List<Event>> ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(Players player);
        int? ReturnOptionSelectedByPlayer(List<List<Event>> allEvents);
    }
}