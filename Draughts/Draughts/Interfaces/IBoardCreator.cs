using System.Collections.Generic;
using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IBoardCreator
    {
        Dictionary<(int, int), Field> GetBoard();
        int GetBoardSize();
        int ReturnNumberOfPawnOnTheBoard();
        Players ReturnPlayerNameFromTheField((int x, int y) position);
    }
}