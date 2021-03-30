using System;
using BoardGame.Interfaces;
using BoardGame.Managers;
using EventHandler = BoardGame.Managers.EventHandler;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(), new Validator(), new Player());
            game.RunBoardBuilder(new BoardBuilder(game.ObjectFactory.Get<IEventHandler>(), game.ObjectFactory.Get<IValidatorWall>()).WithSize(5).AddWall("W 1 1 2 1").AddWall("W 1 1 2 1").BuildBoard());
            game.PlayTheGame(new []{"PMLMMRRMMMM"}); 
            Console.WriteLine("I did nothing.");
        }
    }
}