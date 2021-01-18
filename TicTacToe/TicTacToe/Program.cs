using TicTacToe.Interfaces;
using TicTacToe.Managers;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new Game(new Output(), new Result(), new Validator());
            game.PlayGameInConsole();
        }
    }
}