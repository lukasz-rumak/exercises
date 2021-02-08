using System;
using BoardGame.Managers;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameMaster(new Validator(), new BoardBuilder().WithSize(5).AddWall("W 1 1 2 1").BuildBoard(), new ConsoleOutput());
            game.PlayTheGame(new []{"PMLMMRRMMMM"}); 
            Console.WriteLine("I did nothing.");
        }
    }
}