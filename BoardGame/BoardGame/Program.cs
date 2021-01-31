using System;
using BoardGame.Managers;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameMaster(new Validator(), new BoardBuilder().WithSize(5).BuildBoard(), new ConsoleOutput(), new PawnFactory(), new KnightFactory());
            game.PlayTheGame(new []{"MMMMMM", "RMLMLMMRMMM"});
            Console.WriteLine("I did nothing.");
        }
    }
}