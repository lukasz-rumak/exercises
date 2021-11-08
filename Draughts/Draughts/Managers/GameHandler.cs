using System;
using System.Collections.Generic;
using Draughts.Interfaces;
using Draughts.Models;

namespace Draughts.Managers
{
    public class GameHandler : IGameHandler
    {
        private readonly IBoardCreator _board;
        private readonly IOutput _output;
        private readonly IMovement _movement;
        private readonly IEventsHandler _eventsHandler;
        private readonly IHumanPlayer _humanHandler;

        public GameHandler(IBoardCreator board, IOutput output, IMovement movement, IEventsHandler eventsHandler, IHumanPlayer humanHandler)
        {
            _board = board;
            _output = output;
            _movement = movement;
            _eventsHandler = eventsHandler;
            _humanHandler = humanHandler;
        }

        public void PlayTheGame(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.ComputerVersusComputer:
                    ComputerVersusComputerGame(gameMode);
                    break;
                case GameMode.HumanVersusComputer:
                    HumanVersusComputerGame(gameMode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null);
            }
        }

        private void ComputerVersusComputerGame(GameMode gameMode)
        {
            var player = Players.Player1;
            var gameOver = false;
            GenerateInitOutput();
            
            while (!gameOver)
            {
                if (CheckConditionIfGameShouldEndDueToNoPawns())
                {
                    gameOver = true;
                    continue;
                }
                
                GenerateStartRoundOutput(gameMode, player);
                var events = ExecuteComputerPlayerMovement(player);
                GenerateEndRoundOutput(gameMode, player, events);
                player = player == Players.Player1 ? Players.Player2 : Players.Player1;
                gameOver = CheckConditionIfGameShouldEndDueToNoEvents(events);
            }
            
            Console.WriteLine("GAME OVER");
        }

        private void HumanVersusComputerGame(GameMode gameMode)
        {
            var player = Players.Player1;
            var gameOver = false;
            
            while (!gameOver)
            {
                if (CheckConditionIfGameShouldEndDueToNoPawns())
                {
                    gameOver = true;
                    continue;
                }
                
                GenerateStartRoundOutput(gameMode, player);
                var events = new List<Event>();
                
                if (player == Players.Player1)
                {
                    var allEvents = _humanHandler.ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(player);
                    if (allEvents == null)
                    {
                        player = SkipPlayer1Round();
                        continue;
                    }
                    
                    var option = _humanHandler.ReturnOptionSelectedByPlayer(allEvents);
                    if (option == null)
                    {
                        player = SkipPlayer1Round();
                        continue;
                    }
                    
                    events = allEvents[option.Value];
                    _movement.MovePawn(_board.GetBoard(), player, events);
                }
                else
                {
                    events = ExecuteComputerPlayerMovement(player);
                }
                
                GenerateEndRoundOutput(gameMode, player, events);
                player = player == Players.Player1 ? Players.Player2 : Players.Player1;
                gameOver = CheckConditionIfGameShouldEndDueToNoEvents(events);
            }
        }

        private List<Event> ExecuteComputerPlayerMovement(Players player)
        {
            var events = _eventsHandler.ReturnTheBestEventsToExecute(player);
            _movement.MovePawn(_board.GetBoard(), player, events);
            
            return events;
        }

        private void GenerateInitOutput()
        {
            Console.WriteLine("GAME STARTED");
            _output.GenerateBoardVisualization(_board);
        }           
        
        private void GenerateStartRoundOutput(GameMode gameMode, Players player)
        {
            Console.WriteLine("NEXT ROUND");
            Console.WriteLine($"{player}");
            if (gameMode == GameMode.HumanVersusComputer && player == Players.Player1)
                _output.GenerateBoardVisualization(_board);
        }

        private void GenerateEndRoundOutput(GameMode gameMode, Players player, List<Event> events)
        {
            _output.GeneratePlayerMovement(player, events);
            if (gameMode == GameMode.HumanVersusComputer && player == Players.Player2)
                return;
            _output.GenerateBoardVisualization(_board);
            _output.GenerateSummary(_board);
        }
        
        private bool CheckConditionIfGameShouldEndDueToNoPawns()
        {
            return _board.ReturnNumberOfPawnOnTheBoard() < 2;
        }
        
        private bool CheckConditionIfGameShouldEndDueToNoEvents(List<Event> events)
        {
            return events.Count == 0;
        }
        
        private Players SkipPlayer1Round()
        {
            Console.WriteLine("Skipping player1 round");
            return Players.Player2;
        }
    }
}