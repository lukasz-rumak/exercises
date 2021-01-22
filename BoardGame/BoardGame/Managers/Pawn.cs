using System;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Pawn : IPawn
    {
        public int PawnId { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }
        
        public Pawn(int pawnId)
        {
            PawnId = pawnId;
            IsAlive = true;
            Position = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.North
            };
        }
        
        public Direction ChangeDirectionToRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.East:
                    return Direction.South;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                case Direction.None:
                    return Direction.None;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Direction ChangeDirectionToLeft(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.West;
                case Direction.West:
                    return Direction.South;
                case Direction.South:
                    return Direction.East;
                case Direction.East:
                    return Direction.North;
                case Direction.None:
                    return Direction.None;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}