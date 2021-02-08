using System.Collections.Generic;
using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGameBoard
    {
        Field[,] Board { get; set; }
        int WithSize { get; set; }
        List<Wall> Walls { get; set; }
        Field[,] GenerateBoard(int size);
        void AddWallsToBoard(string instruction);
        void ExecuteThePlayerInstruction(IPiece piece, char instruction);
    }
}