using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameBoard : IGameBoard
    {
        public Field[,] Board { get; set; }
        public int WithSize { get; set; }
        public List<Wall> Walls { get; set; }

        public Field[,] GenerateBoard(int size)
        {
            var board = new Field[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = new Field(i, j);
            return board;
        }

        public List<Wall> AddWallsToBoardTmp(int size)
        {
            return new List<Wall>
            {
                new Wall
                {
                    WallPositionX = (0, 1),
                    WallPositionY = (1, 2)
                }
            };
        }

        public bool WatchOutForWallsTmp(Direction direction, int x, int y)
        {
            if (direction == Direction.North)
            {
                for (int i = 0; i < Walls.Count; i++)
                {
                    if ((y, y + 1) == Walls[i].WallPositionY)
                        return false;
                }
            }
            
            return true;
        }

        public void ExecuteThePlayerInstruction(IPiece piece, char instruction)
        {
            if (instruction == 'M')
                MovePiece(piece);
            else if (instruction == 'R')
                piece.ChangeDirectionToRight();
            else if (instruction == 'L')
                piece.ChangeDirectionToLeft();
        }

        private void MovePiece(IPiece piece)
        {
            if (!IsMovePossible(piece.Position.Direction, piece.Position.X, piece.Position.Y)) return;
            MarkFieldAsNotTaken(piece.Position.X, piece.Position.Y);
            if (piece.Position.Direction == Direction.North)
            {
                piece.Position.Y += 1;
            }
            else if (piece.Position.Direction == Direction.NorthEast)
            {
                piece.Position.X += 1;
                piece.Position.Y += 1;
            }
            else if (piece.Position.Direction == Direction.East)
            {
                piece.Position.X += 1;
            }
            else if (piece.Position.Direction == Direction.SouthEast)
            {
                piece.Position.X += 1;
                piece.Position.Y -= 1;
            }
            else if (piece.Position.Direction == Direction.South)
            {
                piece.Position.Y -= 1;
            }
            else if (piece.Position.Direction == Direction.SouthWest)
            {
                piece.Position.X -= 1;
                piece.Position.Y -= 1;
            }
            else if (piece.Position.Direction == Direction.West)
            {
                piece.Position.X -= 1;
            }
            else if (piece.Position.Direction == Direction.NorthWest)
            {
                piece.Position.X -= 1;
                piece.Position.Y += 1;
            }
                
            MarkFieldAsTaken(piece);
        }

        private bool IsMovePossible(Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.North:
                    return y + 1 < WithSize && !Board[x, y + 1].IsTaken;
                case Direction.NorthEast:
                    return x + 1 < WithSize && y + 1 < WithSize && !Board[x + 1, y + 1].IsTaken;
                case Direction.East:
                    return x + 1 < WithSize && !Board[x + 1, y].IsTaken;
                case Direction.SouthEast:
                    return x + 1 < WithSize && y - 1 >= 0 && !Board[x + 1, y - 1].IsTaken;
                case Direction.South:
                    return y - 1 >= 0 && !Board[x, y - 1].IsTaken;
                case Direction.SouthWest:
                    return x - 1 >= 0 && y - 1 >= 0 && !Board[x - 1, y - 1].IsTaken;
                case Direction.West:
                    return x - 1 >= 0 && !Board[x - 1, y].IsTaken;
                case Direction.NorthWest:
                    return x - 1 >= 0 && y + 1 < WithSize && !Board[x - 1, y + 1].IsTaken;
                case Direction.None:
                    return false;
                default:
                    return false;
            }
        }

        private void MarkFieldAsTaken(IPiece piece)
        {
            Board[piece.Position.X, piece.Position.Y].TakenBy = piece;
        }
        
        private void MarkFieldAsNotTaken(int x, int y)
        {
            Board[x, y].TakenBy = null;
        }
    }
}