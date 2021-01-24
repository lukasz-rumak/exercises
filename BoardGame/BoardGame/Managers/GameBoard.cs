using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameBoard : IGameBoard
    {
        public Field[,] Board { get; set; }
        public int WithSize { get; set; }

        public Field[,] GenerateBoard(int size)
        {
            var board = new Field[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = new Field(i, j);
            return board;
        }

        public void ExecuteThePlayerInstruction(Pawn pawn, char instruction)
        {
            if (instruction == 'M')
                MovePawn(pawn);
            else if (instruction == 'R')
                pawn.ChangeDirectionToRight();
            else if (instruction == 'L')
                pawn.ChangeDirectionToLeft();
        }

        private void MovePawn(Pawn pawn)
        {
            if (!IsMovePossible(pawn.Position.Direction, pawn.Position.X, pawn.Position.Y)) return;
            MarkFieldAsNotTaken(pawn.Position.X, pawn.Position.Y);
            if (pawn.Position.Direction == Direction.North)
                pawn.Position.Y += 1;
            else if (pawn.Position.Direction == Direction.East)
                pawn.Position.X += 1;
            else if (pawn.Position.Direction == Direction.South)
                pawn.Position.Y -= 1;
            else if (pawn.Position.Direction == Direction.West)
                pawn.Position.X -= 1;
            MarkFieldAsTaken(pawn);
        }
        
        private bool IsMovePossible(Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.North:
                    return y + 1 < WithSize && !Board[x, y + 1].IsTaken;
                case Direction.East:
                    return x + 1 < WithSize && !Board[x + 1, y].IsTaken;
                case Direction.South:
                    return y - 1 >= 0 && !Board[x, y - 1].IsTaken;
                case Direction.West:
                    return x - 1 >= 0 && !Board[x - 1, y].IsTaken;
                case Direction.None:
                    return false;
                default:
                    return false;
            }
        }

        private void MarkFieldAsTaken(Pawn pawn)
        {
            Board[pawn.Position.X, pawn.Position.Y].TakenBy = pawn;
        }
        
        private void MarkFieldAsNotTaken(int x, int y)
        {
            Board[x, y].TakenBy = null;
        }
    }
}