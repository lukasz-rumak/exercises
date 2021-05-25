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
            var game = new GameMaster(new ConsoleOutput(), new EventHandler(new ConsoleOutput()), new Validator(),
                new Validator(), new Validator(), new PlayersHandler(), new BerryCreator(), new AStarPathFinderAdapter(new AStarPathFinderAlgorithm()));
            game.RunBoardBuilder(
                new BoardBuilder(game.ObjectFactory.Get<IEventHandler>(), game.ObjectFactory.Get<IValidatorWall>(),
                        game.ObjectFactory.Get<IValidatorBerry>(), game.ObjectFactory.Get<IBerryCreator>(),
                        game.ObjectFactory.Get<IAStarPathFinderAdapter>()).WithSize(5)
                    .AddWall("W 1 1 2 1").AddWall("W 1 1 2 1").AddWall("W 0 3 0 2").AddWall("W 0 4 1 4")
                    .AddWall("W 0 4 1 3").AddWall("W 1 4 0 3")
                    .AddWall("W 3 4 4 4").AddWall("W 3 3 4 4").AddWall("W 4 3 4 4").BuildBoard());

            game.PlayTheGame(new[] {"PMMMM"});
            Console.WriteLine("I did nothing.");
        }
    }
}