using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPresentation
    {
        void GenerateOutput(IGameBoard board, IReadOnlyList<IPiece> pawns);
    }
}