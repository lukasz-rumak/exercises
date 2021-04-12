using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGameBoard
    {
        int WithSize { get; set; }
        void GenerateBoard(int size);
        void CreateWallOnBoard(Wall wallToAdd);
        void CreateBerryOnBoard(IBerry berryToAdd);
        bool IsFieldTaken(int x, int y);
        int ReturnPieceIdFromTakenField(int x, int y);
        void MarkFieldAsTakenByNewPiece(IPiece piece);
        void ExecuteThePlayerInstruction(IPiece piece, char instruction);
    }
}