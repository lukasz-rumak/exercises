using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Knight : IPiece
    {
        public int PieceId { get; set; }
        public string PieceType { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }

        public Knight(int knightId)
        {
            PieceId = knightId;
            PieceType = "Knight";
            IsAlive = true;
            Position = new Position
            {
                X = knightId,
                Y = knightId,
                Direction = Direction.NorthEast
            };
        }

        public IPosition MovePiece()
        {
            return null;
        }

        public void ChangeDirectionToRight()
        {
            Position.Direction = Position.Direction switch
            {
                Direction.NorthEast => Direction.SouthEast,
                Direction.SouthEast => Direction.SouthWest,
                Direction.SouthWest => Direction.NorthWest,
                Direction.NorthWest => Direction.NorthEast,
                _ => Position.Direction
            };
        }

        public void ChangeDirectionToLeft()
        {
            Position.Direction = Position.Direction switch
            {
                Direction.NorthWest => Direction.SouthWest,
                Direction.SouthWest => Direction.SouthEast,
                Direction.SouthEast => Direction.NorthEast,
                Direction.NorthEast => Direction.NorthWest,
                Direction.None => Direction.None,
                _ => Position.Direction
            };
        }
    }
}