using System;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameMaster();
            game.RunBoardBuilder(new BoardBuilder(game.ObjectFactory.Get<IEvent>(), game.ObjectFactory.Get<IValidatorWall>()).WithSize(5).AddWall("W 1 1 2 1").AddWall("W 1 1 2 1").BuildBoard());
            game.PlayTheGame(new []{"PMLMMRRMMMM"}); 
            Console.WriteLine("I did nothing.");
        }
    }
}