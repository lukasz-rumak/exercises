using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IMovement
    {
        void MovePawn(Dictionary<(int, int), Field> board, Players player, List<Event> events);
    }
}