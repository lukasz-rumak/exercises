using System;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Pawn : IPawn
    {
        public int PawnId { get; set; }
        public bool IsAlive { get; set; }
        public IPosition Position { get; }
        
        private readonly IValidator _validator;
        private readonly IGameBoard _board;

        public Pawn(IValidator validator, IGameBoard board, int pawnId)
        {
            _validator = validator;
            _board = board;
            Position = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.North
            };
            PawnId = pawnId;
            IsAlive = true;
        }
        public void ExecuteThePlayerInstruction(string input)
        {
            if (!_validator.ValidateInput(input))
            {
                IsAlive = false;
                return;
            }

            foreach (var instruction in input)
            {
                if (instruction == 'M')
                {
                    (Position.X, Position.Y) = MakeMove(_board.WithSize, Position.Direction, Position.X, Position.Y);
                }
                else
                {
                    Position.Direction = instruction == 'R' ? ChangeDirectionToRight(Position.Direction) : ChangeDirectionToLeft(Position.Direction);
                }
            }
            
            //return ConvertResult(_board.Board, Position.Direction, Position.X, Position.Y);
        }

        private string ConvertResult(Field[,] board, Direction direction, int x, int y)
        {
            return new StringBuilder().Append(board[x, y].X)
                .Append(" ")
                .Append(board[x, y].Y)
                .Append(" ")
                .Append(direction)
                .ToString();
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

//        public int MakeMoveObsolete(Direction direction, int position)
//        {
//            switch (direction)
//            {
//                case Direction.North:
//                    if (position + _size > _boardLength)
//                        return position;
//                    else
//                        return position + _size;
//                case Direction.East:
//                    return CalculateEastOrWest(Direction.East, position);
//                case Direction.South:
//                    if (position - _size < 0)
//                        return position;
//                    else
//                        return position - _size;
//                case Direction.West:
//                    return CalculateEastOrWest(Direction.West, position);
//                case Direction.None:
//                    return position;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//        }
//
//        private int CalculateEastOrWestObsolete(Direction direction, int position)
//        {
//            var start = 0;
//            var end = _size - 1;
//            for (int i = 0; i < _size; i++)
//            {
//                if (position >= start && position <= end)
//                {
//                    switch (direction)
//                    {
//                        case Direction.East:
//                            if (position + 1 > end)
//                                return position;
//                            return position + 1;
//                        case Direction.West:
//                            if (position - 1 < start)
//                                return position;
//                            return position - 1;
//                    }
//                }
//                start += _size;
//                end += _size;
//            }
//
//            return position;
//        }