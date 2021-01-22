using System;
using BoardGame.Managers;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameMaster(new Validator(), new BoardBuilder().WithSize(5).GenerateBoard().Build(), new ConsoleOutput());
            game.PlayTheGame(new []{"MMMM", "RMMMM"});
            Console.WriteLine("I did nothing.");
        }
    }
}