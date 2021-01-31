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
        
        public (int, int) CalculatePieceNewPosition()
        {
            return Position.Direction switch
            {
                Direction.NorthEast => (Position.X + 1, Position.Y + 1),
                Direction.SouthEast => (Position.X + 1, Position.Y - 1),
                Direction.SouthWest => (Position.X - 1, Position.Y - 1),
                Direction.NorthWest => (Position.X - 1, Position.Y + 1),
                Direction.None => (Position.X, Position.Y),
                _ => (Position.X, Position.Y)
            };
        }
        
        public void ChangePiecePosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
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