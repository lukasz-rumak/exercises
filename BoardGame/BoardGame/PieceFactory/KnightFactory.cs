using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class KnightAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Knight(pieceId);
        }
    }
}