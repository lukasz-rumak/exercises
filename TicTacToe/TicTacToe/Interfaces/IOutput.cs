using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IOutput
    {
        void ShowCurrentBoardStatus(Field[,] board);
    }
}