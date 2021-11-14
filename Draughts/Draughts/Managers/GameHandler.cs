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
        private readonly Dictionary<GameMode, Func<Players, List<Event>>> _executeGameMode;
        private Players _humanPlayer;

        public GameHandler(IBoardCreator board, IOutput output, IMovement movement, IEventsHandler eventsHandler, IHumanPlayer humanHandler)
        {
            _board = board;
            _output = output;
            _movement = movement;
            _eventsHandler = eventsHandler;
            _humanHandler = humanHandler;
            _executeGameMode = CreateDictGameModel();
            _humanPlayer = Players.None;
        }

        public void PlayTheGame()
        {
            var roundOwner = Players.Player1;
            var gameMode = _humanHandler.ReturnSelectedGameMode();
            if (gameMode == GameMode.HumanVersusComputer)
                _humanPlayer = _humanHandler.ReturnSelectedPlayer();
            if (gameMode == GameMode.HumanVersusHuman)
                _humanPlayer = roundOwner;
            
            var gameOver = false;
            GenerateInitOutput();

            while (!gameOver)
            {
                if (CheckConditionIfGameShouldEndDueToNoPawns())
                {
                    gameOver = true;
                    continue;
                }

                GenerateStartRoundOutput(gameMode, roundOwner);

                var events = _executeGameMode[gameMode](roundOwner);
                if (events == null && roundOwner == _humanPlayer)
                {
                    roundOwner = SkipHumanPlayerRound();
                    continue;
                }

                GenerateEndRoundOutput(gameMode, roundOwner, events);
                roundOwner = roundOwner == Players.Player1 ? Players.Player2 : Players.Player1;
                if (gameMode == GameMode.HumanVersusHuman)
                    _humanPlayer = roundOwner;
                gameOver = CheckConditionIfGameShouldEndDueToNoEvents(events);
            }

            Console.WriteLine("GAME OVER");
        }

        private Dictionary<GameMode, Func<Players, List<Event>>> CreateDictGameModel()
        {
            return new Dictionary<GameMode, Func<Players, List<Event>>>
            {
                [GameMode.ComputerVersusComputer] = ExecuteComputerPlayerMovement,
                [GameMode.HumanVersusComputer] = HumanVersusComputerGame,
                [GameMode.HumanVersusHuman] = HumanVersusHumanGame
            };
        }

        private List<Event> HumanVersusHumanGame(Players player)
        {
            return ExecuteHumanPlayerMovement(player);
        }

        private List<Event> HumanVersusComputerGame(Players player)
        {
            return player == _humanPlayer ? ExecuteHumanPlayerMovement(player) : ExecuteComputerPlayerMovement(player);
        }

        private List<Event> ExecuteHumanPlayerMovement(Players player)
        {
            var allEvents = _humanHandler.ReadPawnPositionFromConsoleAndReturnAllEventsForGivenPawn(player, _humanPlayer);
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
            if ((gameMode == GameMode.HumanVersusComputer && player == _humanPlayer) 
                || gameMode == GameMode.HumanVersusHuman)
                _output.GenerateBoardVisualization(_board);
        }

        private void GenerateEndRoundOutput(GameMode gameMode, Players player, List<Event> events)
        {
            _output.GeneratePlayerMovement(player, events);
            if (gameMode == GameMode.HumanVersusComputer && player != _humanPlayer)
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
        
        private Players SkipHumanPlayerRound()
        {
            Console.WriteLine("Skipping human player round");
            return _humanPlayer == Players.Player1 ? Players.Player2 : Players.Player1;
        }
    }
}