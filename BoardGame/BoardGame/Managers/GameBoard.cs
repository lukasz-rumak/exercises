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
            var (newX, newY) = piece.CalculatePieceNewPosition();
            if (!IsMovePossible(newX, newY)) return;
            MarkFieldAsNotTaken(piece.Position.X, piece.Position.Y);
            piece.ChangePiecePosition(newX, newY);
            MarkFieldAsTaken(piece);
        }

        private bool IsMovePossible(int x, int y)
        {
            return IsInBoundaries(x, y) && IsFieldFree(x, y);
        }

        private bool IsInBoundaries(int x, int y)
        {
            return x < WithSize && y < WithSize && x >= 0 && y >= 0;
        }

        private bool IsFieldFree(int x, int y)
        {
            return !Board[x, y].IsTaken;
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