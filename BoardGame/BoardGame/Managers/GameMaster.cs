using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.PieceFactory;

namespace BoardGame.Managers
{
    public class GameMaster : IGame, IGameApi
    {
        public ObjectFactory.ObjectFactory ObjectFactory { get; set; }

        private readonly IEventHandler _eventHandler;
        private readonly IValidator _validator;
        private IBoardBuilder _boardBuilder;
        private IGameBoard _board;
        private readonly IPlayer _playersHandler;
        private readonly IPresentation _presentation;
        private readonly PieceFactory.PieceFactory _pieceFactory;
        private readonly List<string> _gameResult;
        private bool _isBoardBuilt;
        private bool _isGameComplete;

        public GameMaster(IPresentation presentation, IEventHandler eventHandler, IValidator validator, IValidatorWall validatorWall, IValidatorBerry validatorBerry, IPlayer player, IBerryCreator berryCreator, IAStarPathFinderAlgorithm aStarPathFinder)
        {
            ObjectFactory = new ObjectFactory.ObjectFactory();
            ObjectFactory.Register<IPresentation>(presentation);
            ObjectFactory.Register<IEventHandler>(eventHandler);//(ObjectFactory.Get<IPresentation>()));
            ObjectFactory.Register<IValidator>(validator);
            ObjectFactory.Register<IValidatorWall>(validatorWall);
            ObjectFactory.Register<IValidatorBerry>(validatorBerry);
            ObjectFactory.Register<IPlayer>(player);
            ObjectFactory.Register<IBerryCreator>(berryCreator);
            ObjectFactory.Register<IAStarPathFinderAlgorithm>(aStarPathFinder);
            _presentation = ObjectFactory.Get<IPresentation>();
            _eventHandler = ObjectFactory.Get<IEventHandler>();
            _validator = ObjectFactory.Get<IValidator>();
            _playersHandler = ObjectFactory.Get<IPlayer>();
            _pieceFactory = new PieceFactory.PieceFactory();
            _pieceFactory.Register("P", new PawnAbstractFactory());
            _pieceFactory.Register("K", new KnightAbstractFactory());
            _validator.AllowedPieceTypes = _pieceFactory.GetRegisteredKeys();
            _gameResult = new List<string>();
            _isBoardBuilt = false;
            _isGameComplete = false;
        }

        public void RunBoardBuilder(IGameBoard board)
        {
            _board = board;
            _isBoardBuilt = true;
            _eventHandler.PublishEvent(EventType.BoardBuilt, "");
        }

        public void StartBoardBuilder(IBoardBuilder board)
        {
            _boardBuilder = board;
        }

        public void AddWallToBoard(string wallCoordinates)
        {
            if (_isGameComplete) return;
            _boardBuilder.AddWall(wallCoordinates);
        }

        public void AddBerryToBoard(string berryCoordinates)
        {
            if (_isGameComplete) return;
            _boardBuilder.AddBerry(berryCoordinates);
        }
        
        public void FinaliseBoardBuilder()
        {
            if (_isGameComplete) return;
            _board = _boardBuilder.BuildBoard();
            _isBoardBuilt = true;
            _eventHandler.PublishEvent(EventType.BoardBuilt, "");
        }

        public bool IsBoardBuilt()
        {
            return _isBoardBuilt;
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
            if (_isGameComplete) return;
            _playersHandler.CreatePlayers(_board, _pieceFactory, instructions);
            _eventHandler.PublishEvent(EventType.PlayerAdded, "");
        }
        
        public void MovePlayer(IReadOnlyList<string> instructions, int playerId)
        {
            if (_isGameComplete) return;
            if (_playersHandler.ReturnPlayersNumber() - 1 < playerId)
            {
                _eventHandler.PublishEvent(EventType.IncorrectPlayerId, $"The requested player id: {playerId}");
                return;
            }
            ExecuteValidation(new List<IPiece>{_playersHandler.ReturnPlayerInfo(playerId)}, instructions);
            ExecuteTheInstructions(new List<IPiece>{_playersHandler.ReturnPlayerInfo(playerId)}, instructions);
        }
        
        public void MovePlayers(IReadOnlyList<string> instructions)
        {
            if (_isGameComplete) return;
            ExecuteValidation(_playersHandler.ReturnPlayersInfo(), instructions);
            ExecuteTheInstructions(_playersHandler.ReturnPlayersInfo(), instructions);
        }

        public IPiece GetPlayerInfo(int playerId)
        {
            return _playersHandler.ReturnPlayerInfo(playerId);
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
            var output = _presentation.GenerateOutputApi(_board, _playersHandler.ReturnPlayersInfo());
            if (!string.IsNullOrWhiteSpace(output))
                _eventHandler.PublishEvent(EventType.GeneratedBoardOutput, "");
            return output;
        }

        public bool IsGameComplete()
        {
            return _isGameComplete;
        }

        public void MarkGameAsComplete()
        {
            _isGameComplete = true;
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
                            if (_board.CheckIfAllBerriesCollected()) return;
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
            var players = _playersHandler.ReturnPlayersInfo();
            foreach (var player in players)
            {
                _gameResult.Add(player.IsAlive
                    ? new StringBuilder().Append("[P] ").Append(player.Position.X).Append(" ").Append(player.Position.Y)
                        .Append(" ").Append(player.Position.Direction).Append(" ").Append("[S] ")
                        .Append(player.CalculateScore()).ToString()
                    : @"Instruction not clear. Exiting...");
            }
        }
    }
}