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
        
        private readonly IGameBoard _board;

        public Pawn(IGameBoard board, int pawnId)
        {
            PawnId = pawnId;
            IsAlive = true;
            Position = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.North
            };
            _board = board;
        }
        public void ExecuteThePlayerInstruction(char instruction)
        {
            if (instruction == 'M')
                (Position.X, Position.Y) = MakeMove(_board.WithSize, Position.Direction, Position.X, Position.Y);
            else
                Position.Direction = instruction == 'R' ? ChangeDirectionToRight(Position.Direction) : ChangeDirectionToLeft(Position.Direction);
        }

        private (int, int) MakeMove(int boardSize, Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.North:
                    return y + 1 < boardSize ? (x, y + 1) : (x, y);
                case Direction.East:
                    return x + 1 < boardSize ? (x + 1, y) : (x, y);
                case Direction.South:
                    return y - 1 >= 0 ? (x, y - 1) : (x, y);
                case Direction.West:
                    return x - 1 >= 0 ? (x - 1, y) : (x, y);
                case Direction.None:
                    return (x, y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Direction ChangeDirectionToRight(Direction direction)
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

        private Direction ChangeDirectionToLeft(Direction direction)
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