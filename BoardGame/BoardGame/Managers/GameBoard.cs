using System.Collections.Generic;
using System.Linq;
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
            if (!IsMovePossible(piece.Position.X, piece.Position.Y, newX, newY)) return;
            MarkFieldAsNotTaken(piece.Position.X, piece.Position.Y);
            piece.ChangePiecePosition(newX, newY);
            MarkFieldAsTaken(piece);
        }

        private bool IsMovePossible(int currentX, int currentY, int newX, int newY)
        {
            return IsInBoundaries(newX, newY) && IsFieldFree(newX, newY) && !IsWallOnTheRoute(currentX, currentY, newX, newY);
        }

        private bool IsInBoundaries(int x, int y)
        {
            return x < WithSize && y < WithSize && x >= 0 && y >= 0;
        }

        private bool IsFieldFree(int x, int y)
        {
            return !Board[x, y].IsTaken;
        }

        private bool IsWallOnTheRoute(int currentX, int currentY, int newX, int newY)
        {
            return Walls != null && Walls.Any(wall =>
                       wall.WallPositionField1.Item1 == currentX && wall.WallPositionField1.Item2 == currentY &&
                       wall.WallPositionField2.Item1 == newX && wall.WallPositionField2.Item2 == newY);
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