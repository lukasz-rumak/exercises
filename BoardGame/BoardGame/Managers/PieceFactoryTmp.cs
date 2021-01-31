using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public abstract class PieceFactoryTmp
    {
        public abstract IPiece CreatePiece(int pieceId);
    }
}