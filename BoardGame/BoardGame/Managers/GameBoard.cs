using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameBoard : IGameBoard
    {
        public Field[,] Board { get; set; }
        public int WithSize { get; set; }
        public List<Wall> Walls { get; set; }
        
        private readonly IEvent _eventHandler;
        private readonly IValidator _validator;

        public GameBoard(IEvent eventHandler, IValidator validator)
        {
            _eventHandler = eventHandler;
            _validator = validator;
            Walls = new List<Wall>();
        }
        
        public Field[,] GenerateBoard(int size)
        {
            var board = new Field[size, size];
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                board[i, j] = new Field(i, j);
            return board;
        }
        
        public void AddWallsToBoard(string instruction)
        {
            if (!_validator.ValidateWallsInput(instruction, WithSize))
            {
                _eventHandler.Events[EventType.WallCreationError]("");
                return;
            }

            var stringBuilder = new StringBuilder();
            foreach (var c in instruction.Where(c => c != ' '))
                stringBuilder.Append(c);
            var wallsToBuild = stringBuilder.ToString().Remove(0, 1).Split("W");
            foreach (var coordinates in wallsToBuild)
                CreateWall(coordinates);
        }

        private void CreateWall(string coordinates)
        {
            Walls.Add(
                new Wall
                {
                    WallPositionField1 = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                    WallPositionField2 = (int.Parse(coordinates[2].ToString()), int.Parse(coordinates[3].ToString()))
                }
            );
//            Walls.Add(new Wall
//            {
//                WallPositionField1 = (int.Parse(coordinates[2].ToString()), int.Parse(coordinates[3].ToString())),
//                WallPositionField2 = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString()))
//            });
        }
        
        public void NewCreateWall(Wall wallToAdd) // zmien tez nazwe metody
        {
            Walls.Add(wallToAdd);
            Walls.Add(wallToAdd.ReversedWall());
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
            if (!IsMovePossible(piece, newX, newY)) return;
            MarkFieldAsNotTaken(piece.Position.X, piece.Position.Y);
            piece.ChangePiecePosition(newX, newY);
            MarkFieldAsTaken(piece);
        }

        private bool IsMovePossible(IPiece piece, int newX, int newY)
        {
            return IsInBoundaries(newX, newY) && IsFieldFree(newX, newY) &&
                    IsRouteEmpty(piece, newX, newY);
        }

        private bool IsInBoundaries(int x, int y)
        {
            if (x < WithSize && y < WithSize && x >= 0 && y >= 0)
                return true;
            _eventHandler.Events[EventType.OutsideBoundaries]($"({x}, {y})");
            return false;
        }

        private bool IsFieldFree(int x, int y)
        {
            if (!Board[x, y].IsTaken)
                return true;
            _eventHandler.Events[EventType.FieldTaken]($"Field taken: ({x}, {y})");
            return false;
        }

        private bool IsRouteEmpty(IPiece piece, int newX, int newY)
        {
            if (Walls == null) return true;
            if (!Walls.Any(wall =>
                wall.WallPositionField1.Item1 == piece.Position.X &&
                wall.WallPositionField1.Item2 == piece.Position.Y &&
                wall.WallPositionField2.Item1 == newX && wall.WallPositionField2.Item2 == newY))
                return true;
            _eventHandler.Events[EventType.WallOnTheRoute](
                $"PieceId: {piece.PieceId}, PieceType: {piece.PieceType}, move from ({piece.Position.X},{piece.Position.Y}) to ({newX}, {newY})");
            return false;
        }

        private void MarkFieldAsTaken(IPiece piece)
        {
            Board[piece.Position.X, piece.Position.Y].TakenBy = piece;
            _eventHandler.Events[EventType.PieceMove]($"piece moved. PieceId: {piece.PieceId}, PieceType: {piece.PieceType}, new position: ({piece.Position.X},{piece.Position.Y})");
        }
        
        private void MarkFieldAsNotTaken(int x, int y)
        {
            Board[x, y].TakenBy = null;
        }
    }
}