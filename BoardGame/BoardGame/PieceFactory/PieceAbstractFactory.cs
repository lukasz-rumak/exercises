using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public abstract class PieceAbstractFactory
    {
        public abstract IPiece CreatePiece(int pieceId);
    }
}