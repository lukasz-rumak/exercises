using BoardGame.Interfaces;

namespace BoardGame.PieceFactory
{
    public abstract class PieceAbstractFactory
    {
        public abstract IPiece CreatePiece(int pieceId);
    }
}