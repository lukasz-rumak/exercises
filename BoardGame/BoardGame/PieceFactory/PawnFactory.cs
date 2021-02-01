using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class PawnAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Pawn(pieceId);
        }
    }
}