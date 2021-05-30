using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Models.Berries;

namespace BoardGame.Managers
{
    public class Knight : BerryCollector, IPiece
    {
        public int PieceId { get; set; }
        public string PieceType { get; set; }
        public bool IsAlive { get; set; }
        public List<(int, int)> PossibleMoves { get; set; }
        public IPosition Position { get; }
        
        public Knight(int knightId, List<(int, int)> possibleMoves)
        {
            PieceId = knightId;
            PieceType = "Knight";
            IsAlive = true;
            PossibleMoves = possibleMoves;
            Position = new Position
            {
                X = knightId,
                Y = knightId,
                Direction = Direction.NorthEast
            };
        }

        public override int CalculateScore()
        {
            return (CollectedBerries.Count(berry => berry.GetType() == typeof(BlueBerry)) * 2)
                   + CollectedBerries.Count(berry => berry.GetType() == typeof(StrawBerry));
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