using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.PieceFactory
{
    public class KnightAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Knight(pieceId, GetPossibleMoves());
        }

        public override List<(int, int)> GetPossibleMoves()
        {
            return new List<(int, int)>
            {
                (1, 1),
                (1, -1),
                (-1, -1),
                (-1, 1)
            };
        }
    }
}