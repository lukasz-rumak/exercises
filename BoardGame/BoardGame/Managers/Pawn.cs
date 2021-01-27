using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Pawn : IPiece
    {
        public int PieceId { get; set; }
        public Piece PieceType { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }
        
        public Pawn(int pawnId, Piece pieceType)
        {
            PieceId = pawnId;
            PieceType = pieceType;
            IsAlive = true;
            Position = new Position
            {
                X = pawnId,
                Y = pawnId,
                Direction = Direction.North
            };
        }

        public void ChangeDirectionToRight()
        {
            Position.Direction = Position.Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                Direction.None => Direction.None,
                _ => Position.Direction
            };
        }

        public void ChangeDirectionToLeft()
        {
            Position.Direction = Position.Direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                Direction.None => Direction.None,
                _ => Position.Direction
            };
        }
    }
}