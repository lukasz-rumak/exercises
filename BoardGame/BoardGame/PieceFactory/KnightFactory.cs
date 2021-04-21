using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.PieceFactory
{
    public class KnightAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Knight(pieceId);
        }
    }
}