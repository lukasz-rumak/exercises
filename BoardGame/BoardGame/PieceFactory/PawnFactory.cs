using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.PieceFactory
{
    public class PawnAbstractFactory : PieceAbstractFactory
    {
        public override IPiece CreatePiece(int pieceId)
        {
            return new Pawn(pieceId, GetPossibleMoves());
        }

        public override List<(int, int)> GetPossibleMoves()
        {
            return new List<(int, int)>
            {
                (0, 1),
                (1, 0),
                (0, -1),
                (-1, 0)
            };
        }
    }
}