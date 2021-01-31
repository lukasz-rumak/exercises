using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class KnightFactory : PieceFactoryTmp
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Knight(pieceId);
        }
    }
}