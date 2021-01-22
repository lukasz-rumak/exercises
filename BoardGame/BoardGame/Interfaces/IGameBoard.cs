using BoardGame.Managers;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IGameBoard
    {
        Field[,] Board { get; set; }
        int WithSize { get; set; }
        Field[,] GenerateBoard(int size);
        void ExecuteThePlayerInstruction(Pawn pawn, char instruction);
    }
}