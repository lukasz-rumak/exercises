using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.PieceFactory
{
    public abstract class PieceAbstractFactory
    {
        public abstract IPiece CreatePiece(int pieceId);
        public abstract List<(int, int)> GetPossibleMoves();
    }
}