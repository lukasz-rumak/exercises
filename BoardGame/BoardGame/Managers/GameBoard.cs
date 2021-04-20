using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameBoard : IGameBoard
    {
        public int WithSize { get; set; }

        private readonly IEventHandler _eventHandler;
        private Field[,] _board;
        private readonly List<Wall> _walls;
        private readonly List<IBerry> _berries;

        public GameBoard(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
            _walls = new List<Wall>();
            _berries = new List<IBerry>();
        }

        public void GenerateBoard(int size)
        {
            _board = new Field[size, size];
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _board[i, j] = new Field(i, j);
        }

        public void CreateWallOnBoard(Wall wallToAdd)
        {
            _walls.Add(wallToAdd);
            _walls.Add(wallToAdd.ReversedWall());
        }

        public void CreateBerryOnBoard(IBerry berryToAdd)
        {
            _berries.Add(berryToAdd);
        }
        
        public bool CheckIfAllBerriesCollected()
        {
            return _berries.Any() && _berries.All(berry => berry.IsEaten);
        }

        public bool IsNotEatenBerryOnField(int x, int y)
        {
            return _berries.Where(berry => berry.IsEaten == false)
                .Any(berry => berry.BerryPosition.Item1 == x && berry.BerryPosition.Item2 == y);
        }

        public bool IsFieldTaken(int x, int y)
        {
            return _board[x, y].IsTaken;
        }

        public int ReturnPieceIdFromTakenField(int x, int y)
        {
            return _board[x, y].TakenBy.PieceId;
        }

        public void MarkFieldAsTakenByNewPiece(IPiece piece)
        {
            if (IsInBoundaries(piece.Position.X, piece.Position.Y))
                _board[piece.Position.X, piece.Position.Y].TakenBy = piece;
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
            CollectBerryIfAny(piece);
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
            _eventHandler.PublishEvent(EventType.OutsideBoundaries, $"({x}, {y})");
            return false;
        }

        private bool IsFieldFree(int x, int y)
        {
            if (!_board[x, y].IsTaken)
                return true;
            _eventHandler.PublishEvent(EventType.FieldTaken, $"Field taken: ({x}, {y})");
            return false;
        }

        private bool IsRouteEmpty(IPiece piece, int newX, int newY)
        {
            if (_walls == null) return true;
            if (!_walls.Any(wall =>
                wall.WallPositionField1.Item1 == piece.Position.X &&
                wall.WallPositionField1.Item2 == piece.Position.Y &&
                wall.WallPositionField2.Item1 == newX && wall.WallPositionField2.Item2 == newY))
                return true;
            _eventHandler.PublishEvent(EventType.WallOnTheRoute,
                $"PieceId: {piece.PieceId}, PieceType: {piece.PieceType}, move from ({piece.Position.X},{piece.Position.Y}) to ({newX}, {newY})");
            return false;
        }

        private void MarkFieldAsTaken(IPiece piece)
        {
            _board[piece.Position.X, piece.Position.Y].TakenBy = piece;
            _eventHandler.PublishEvent(EventType.PieceMoved,
                $"The piece moved. PieceId: {piece.PieceId}, PieceType: {piece.PieceType}, new position: ({piece.Position.X},{piece.Position.Y})");
        }

        private void MarkFieldAsNotTaken(int x, int y)
        {
            _board[x, y].TakenBy = null;
        }

        private void CollectBerryIfAny(IPiece piece)
        {
            var berry = GetBerryFromFieldIfAny(piece.Position.X, piece.Position.Y);
            if (berry == null ) return;
            piece.CollectBerry(berry);
            MarkBerryAsEaten(berry);
            _eventHandler.PublishEvent(EventType.BerryEaten,
                $"PieceId: {piece.PieceId}, PieceType: {piece.PieceType}, from field ({piece.Position.X},{piece.Position.Y})");
        }

        private IBerry GetBerryFromFieldIfAny(int x, int y)
        {
            return _berries.Where(berry => berry.IsEaten == false)
                .FirstOrDefault(berry =>
                    berry.BerryPosition.Item1 == x && berry.BerryPosition.Item2 == y);
        }

        private void MarkBerryAsEaten(IBerry berry)
        {
            berry.IsEaten = true;
        }
    }
}