using System;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class Pawn : IPlayer
    {
        private readonly int _size;

        public Pawn(int size)
        {
            _size = size;
        }

        public (int, int) MakeMove(Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.North:
                    return y + 1 < _size ? (x, y + 1) : (x, y);
                case Direction.East:
                    return x + 1 < _size ? (x + 1, y) : (x, y);
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