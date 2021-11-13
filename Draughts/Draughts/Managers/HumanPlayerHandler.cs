using System;
using System.Collections.Generic;
using Draughts.Interfaces;
using Draughts.Models;

namespace Draughts.Managers
{
    public class HumanPlayerHandler : IHumanPlayer
    {
        private readonly IBoardCreator _board;
        private readonly IEventsHandler _eventsHandler;

        public HumanPlayerHandler(IBoardCreator board, IEventsHandler eventsHandler)
        {
            _board = board;
            _eventsHandler = eventsHandler;
        }
        
        public GameMode ReturnSelectedGameMode()
        {
            Console.WriteLine("Please select game mode and press enter");
            Console.WriteLine("Option 0: Computer versus computer");
            Console.WriteLine("Option 1: Human versus computer");
            var key= ReturnAndValidateKeyFromConsole();
            if (key != null) return (GameMode)key;
            Console.WriteLine("No game mode selected. The computer versus computer is loaded by default");
            return GameMode.ComputerVersusComputer;
        }
        
        public Players ReturnSelectedPlayer()
        {
            Console.WriteLine("Please select player number for human player and press enter");
            Console.WriteLine("Option 0: Player1");
            Console.WriteLine("Option 1: Player2");
            var key= ReturnAndValidateKeyFromConsole();
            if (key != null) return (Players)key;
            Console.WriteLine("No player selected. The Player1 is loaded by default");
            return Players.Player1;
        }

        public List<List<Event>> ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(Players player, Players humanPlayer)
        {
            for (var i = 0; i < 5; i++)
            {
                var pos = ReadPawnPositionFromConsole(humanPlayer);
                if (pos?.X == null || pos.Y == null)
                    return null;
                
                var allEvents = _eventsHandler.CreateAllEventsForGivenPawn(player, pos.X.Value, pos.Y.Value);
                if (allEvents.Count > 0)
                    return allEvents;
                        
                Console.WriteLine("Given pawn does not have any moves. Please select another pawn");
            }

            return null;
        }
        
        public int? ReturnOptionSelectedByPlayer(List<List<Event>> allEvents)
        {
            Console.WriteLine("Where you want to go?");
            var counter = 0;
            foreach (var e in allEvents)
            { 
                Console.WriteLine($"Option {counter}: {e[^1].Destination} {e[^1].Action}");
                counter++;
            }
            return ReadOptionFromConsole(counter);
        }

        private Position ReadPawnPositionFromConsole(Players humanPlayer)
        {
            var pos = new Position();
            for (var i = 0; i < 5; i++)
            {
                pos.X = ReadPositionFromConsole("X");
                if (pos.X == null) continue;
                pos.Y = ReadPositionFromConsole("Y");
                if (pos.Y == null) continue;
                if (_board.ReturnPlayerNameFromTheField((pos.X.Value, pos.Y.Value)) == humanPlayer)
                    return pos;
                Console.WriteLine("Please enter valid position for pawn");
            }

            return null;
        }

        private int? ReadPositionFromConsole(string str)
        {
            int? key;
            Console.WriteLine($"Please enter {str} position and press enter");
            try
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid position");
                return null;
            }

            return key;
        }

        private int? ReadOptionFromConsole(int optionRange)
        {
            int? key;
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"Please enter option and press enter");
                try
                {
                    key = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }

                if (key.Value < 0 || key.Value >= optionRange)
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }

                return key;
            }

            return null;
        }

        private int? ReturnAndValidateKeyFromConsole()
        {
            for (var i = 0; i < 5; i++)
            {
                var key = ReturnKeyFromConsole();
                if (key == null)
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }

                return key;
            }

            return null;
        }

        private int? ReturnKeyFromConsole()
        {
            int? key;
            try
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return null;
            }
            
            if (key.Value < 0 || key.Value > 1)
                return null;
            
            return key;
        }
    }
}