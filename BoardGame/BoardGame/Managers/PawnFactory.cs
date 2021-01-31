using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class PawnFactory : PieceFactoryTmp
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Pawn(pieceId);
        }
    }
}