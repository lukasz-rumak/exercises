﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        public ObjectFactory.ObjectFactory ObjectFactory { get; set; }
        public GameStatus GameStatus { get; set; }

        private readonly IEvent _eventHandler;
        private readonly IValidator _validator;
        private IGameBoard _board;
        private readonly IPlayer _player;
        private readonly IPresentation _presentation;
        private readonly PieceFactory _pieceFactory;
        private readonly List<string> _gameResult;
        
        public GameMaster(IPresentation presentation, IEvent eventHandler, IValidator validator, IValidatorWall validatorWall, IPlayer player)
        {
            ObjectFactory = new ObjectFactory.ObjectFactory();
            ObjectFactory.Register<IPresentation>(presentation);
            ObjectFactory.Register<IEvent>(eventHandler);//(ObjectFactory.Get<IPresentation>()));
            ObjectFactory.Register<IValidator>(validator);
            ObjectFactory.Register<IValidatorWall>(validatorWall);
            ObjectFactory.Register<IPlayer>(player);
            _presentation = ObjectFactory.Get<IPresentation>();
            _eventHandler = ObjectFactory.Get<IEvent>();
            _validator = ObjectFactory.Get<IValidator>();
            _player = ObjectFactory.Get<IPlayer>();
            _pieceFactory = new PieceFactory();
            _pieceFactory.Register("P", new PawnAbstractFactory());
            _pieceFactory.Register("K", new KnightAbstractFactory());
            _validator.AllowedPieceTypes = _pieceFactory.GetRegisteredKeys();
            _gameResult = new List<string>();
            GameStatus = new GameStatus{ PlayerPosition = new Dictionary<int, string>() };
        }

        public void RunBoardBuilder(IGameBoard board)
        {
            _board = board;
            _eventHandler.Events[EventType.BoardBuilt]("");
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (_board is null)
                return new[] {"Please create board first!"};
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};

            CreatePlayers(instructions);
            MovePlayers(instructions);
            CollectResult();
            return _gameResult.ToArray();
        }

        public void CreatePlayers(IReadOnlyList<string> instructions)
        {
            _player.CreatePlayers(_board, _pieceFactory, instructions);
            _eventHandler.Events[EventType.PlayerAdded]("");
        }
        
        public void MovePlayer(IReadOnlyList<string> instructions, int playerId)
        {
            if (_player.ReturnPlayersNumber() - 1 < playerId)
            {
                _eventHandler.Events[EventType.IncorrectPlayerId]($"The requested player id: {playerId}");
                return;
            }
            ExecuteValidation(new List<IPiece>{_player.ReturnPlayerInfo(playerId)}, instructions);
            ExecuteTheInstructions(new List<IPiece>{_player.ReturnPlayerInfo(playerId)}, instructions);
            UpdateGameStatus(playerId);
        }
        
        public void MovePlayers(IReadOnlyList<string> instructions)
        {
            ExecuteValidation(_player.ReturnPlayersInfo(), instructions);
            ExecuteTheInstructions(_player.ReturnPlayersInfo(), instructions);
        }

        public EventLog GetLastEvent()
        {
            return _eventHandler.EventsLog.LastOrDefault();
        }

        public List<EventLog> GetAllEvents()
        {
            return _eventHandler.EventsLog;
        }

        public string GenerateOutputApi()
        {
            var output = _presentation.GenerateOutputApi(_board, _player.ReturnPlayersInfo());
            if (!string.IsNullOrWhiteSpace(output))
                _eventHandler.Events[EventType.GeneratedBoardOutput]("");
            return output;
        }

        private void ExecuteValidation(IReadOnlyList<IPiece> players, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < players.Count; i++)
                if (!_validator.ValidateInstructionsInput(instructions[i]))
                    players[i].IsAlive = false;
        }

        private void ExecuteTheInstructions(IReadOnlyList<IPiece> players, IReadOnlyList<string> instructions)
        {
            var longestInstruction = GetTheLongestInstruction(players, instructions);
            
            for (var i = 0; i < longestInstruction; i++)
                for (var j = 0; j < players.Count; j++)
                    if (players[j].IsAlive)
                        if (instructions[j].Length > i)
                        {
                            _board.ExecuteThePlayerInstruction(players[j], instructions[j][i]);
                            _presentation.GenerateOutput(_board, players);
                        }
        }

        private int GetTheLongestInstruction(IReadOnlyCollection<IPiece> players, IReadOnlyList<string> instructions)
        {
            var longest = 0;
            for (var i = 0; i < players.Count; i++)
                if (instructions[i].Length > longest)
                    longest = instructions[i].Length;
            return longest;
        }
        
        private void CollectResult()
        {
            var players = _player.ReturnPlayersInfo();
            foreach (var player in players)
            {
                _gameResult.Add(player.IsAlive
                    ? new StringBuilder().Append(player.Position.X).Append(" ").Append(player.Position.Y)
                        .Append(" ").Append(player.Position.Direction).ToString()
                    : @"Instruction not clear. Exiting...");
            }
        }
        
        private void UpdateGameStatus(int playerId)
        {
            var players = _player.ReturnPlayersInfo();
            var position =
                $"{players[playerId].Position.X} {players[playerId].Position.Y} {players[playerId].Position.Direction}";
            if (GameStatus.PlayerPosition.ContainsKey(playerId))
                GameStatus.PlayerPosition[playerId] = position;
            else
                GameStatus.PlayerPosition.Add(playerId, position);
        }
    }
}