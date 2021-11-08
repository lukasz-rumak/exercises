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
        private readonly Dictionary<Players, Func<Players, List<Event>>> _executePlayersMovement;
        private readonly Dictionary<GameMode, Func<Players, List<Event>>> _executeGameMode;

        public GameHandler(IBoardCreator board, IOutput output, IMovement movement, IEventsHandler eventsHandler, IHumanPlayer humanHandler)
        {
            _board = board;
            _output = output;
            _movement = movement;
            _eventsHandler = eventsHandler;
            _humanHandler = humanHandler;
            _executePlayersMovement = CreateDictExecutePlayersMovement();
            _executeGameMode = CreateDictGameModel();
        }

        public void PlayTheGame(GameMode gameMode)
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

                var events = _executeGameMode[gameMode](player);
                if (events == null && player == Players.Player1)
                {
                    player = SkipPlayer1Round();
                    continue;
                }

                GenerateEndRoundOutput(gameMode, player, events);
                player = player == Players.Player1 ? Players.Player2 : Players.Player1;
                gameOver = CheckConditionIfGameShouldEndDueToNoEvents(events);
            }

            Console.WriteLine("GAME OVER");
        }

        private Dictionary<GameMode, Func<Players, List<Event>>> CreateDictGameModel()
        {
            return new Dictionary<GameMode, Func<Players, List<Event>>>
            {
                [GameMode.ComputerVersusComputer] = ExecuteComputerPlayerMovement,
                [GameMode.HumanVersusComputer] = HumanVersusComputerGame
            };
        }

        private List<Event> HumanVersusComputerGame(Players player)
        {
            return _executePlayersMovement[player](player);
        }
        
        private Dictionary<Players, Func<Players, List<Event>>> CreateDictExecutePlayersMovement()
        {
            return new Dictionary<Players, Func<Players, List<Event>>>
            {
                [Players.Player1] = ExecuteHumanPlayerMovement,
                [Players.Player2] = ExecuteComputerPlayerMovement
            };
        }

        private List<Event> ExecuteHumanPlayerMovement(Players player)
        {
            var allEvents = _humanHandler.ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(player);
            if (allEvents == null)
                return null;

            var option = _humanHandler.ReturnOptionSelectedByPlayer(allEvents);
            if (option == null)
                return null;

            var events = allEvents[option.Value];
            _movement.MovePawn(_board.GetBoard(), player, events);

            return events;
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
            Console.WriteLine("Skipping Player1 round");
            return Players.Player2;
        }
    }
}