using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IResult
    {
        Player CheckForWinner(Field[,] board);
    }
}