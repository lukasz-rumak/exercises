using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGameBoard
    {
        Field[,] Board { get; set; }
        int WithSize { get; set; }
        Field[,] GenerateBoard(int size);
        void CreateWallOnBoard(Wall wallToAdd);
        void ExecuteThePlayerInstruction(IPiece piece, char instruction);
    }
}