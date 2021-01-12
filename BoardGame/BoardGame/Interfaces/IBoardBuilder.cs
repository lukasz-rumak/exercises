using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IBoardBuilder
    {
        Field[,] GenerateBoard();
    }
}