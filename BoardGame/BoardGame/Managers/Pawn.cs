using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Pawn : BerryCollector, IPiece
    {
        public int PieceId { get; set; }
        public string PieceType { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }
        
        public Pawn(int pawnId)
        {
            PieceId = pawnId;
            PieceType = "Pawn";
            IsAlive = true;
            Position = new Position
            {
                X = pawnId,
                Y = pawnId,
                Direction = Direction.North
            };
        }

        public (int, int) CalculatePieceNewPosition()
        {
            return Position.Direction switch
            {
                Direction.North => (Position.X, Position.Y + 1),
                Direction.East => (Position.X + 1, Position.Y),
                Direction.South => (Position.X, Position.Y - 1),
                Direction.West => (Position.X - 1, Position.Y),
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