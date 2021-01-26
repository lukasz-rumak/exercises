using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Knight : IPawn
    {
        public int PawnId { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }

        public Knight(int knightId)
        {
            PawnId = knightId;
            IsAlive = true;
            Position = new Position
            {
                X = knightId,
                Y = knightId,
                Direction = Direction.NorthEast
            };
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