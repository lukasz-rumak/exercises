using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IPresentation
    {
        void GenerateOutput(IGameBoard board, IReadOnlyList<IPiece> pieces);
    }
}