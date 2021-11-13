using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IHumanPlayer
    {
        GameMode ReturnSelectedGameMode();
        Players ReturnSelectedPlayer();
        List<List<Event>> ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(Players player, Players humanPlayer);
        int? ReturnOptionSelectedByPlayer(List<List<Event>> allEvents);
        
    }
}