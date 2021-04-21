using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.PieceFactory
{
    public class PawnAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Pawn(pieceId);
        }
    }
}