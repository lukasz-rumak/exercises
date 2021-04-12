using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        private readonly IGameBoard _board;
        private readonly IValidatorBerry _validatorBerry;
        private readonly IValidatorWall _validatorWall;
        private readonly IEventHandler _eventHandler;

        public BoardBuilder(IEventHandler eventHandler, IValidatorWall validatorWall, IValidatorBerry validatorBerry)
        {
            _eventHandler = eventHandler;
            _validatorWall = validatorWall;
            _validatorBerry = validatorBerry;
            _board = new GameBoard(_eventHandler);
        }

        public IBoardBuilder WithSize(int size)
        {
            _board.WithSize = size;
            _eventHandler.PublishEvent(EventType.GameStarted, "");
            return this;
        }

        public IBoardBuilder AddWall(string instruction)
        {
            AddWallToBoard(instruction, _board.WithSize);
            return this;
        }

        public IBoardBuilder AddBerry(string instruction)
        {
            AddBerryToBoard(instruction, _board.WithSize);
            return this;
        }

        public IGameBoard BuildBoard()
        {
            _board.GenerateBoard(_board.WithSize);
            return _board;
        }

        private void AddWallToBoard(string instruction, int boardSize)
        {
            var validationResult = _validatorWall.ValidateWallInputWithReason(instruction, boardSize);
            if (!validationResult.IsValid)
            {
                _eventHandler.PublishEvent(EventType.WallCreationError, $"{validationResult.Reason}");
                return;
            }

            var stringBuilder = new StringBuilder();
            foreach (var c in instruction.Where(c => c != ' '))
                stringBuilder.Append(c);
            var wallsToBuild = stringBuilder.ToString().Remove(0, 1).Split("W");
            foreach (var coordinates in wallsToBuild)
                _board.CreateWallOnBoard(CreateWall(coordinates));

            _eventHandler.PublishEvent(EventType.WallCreationDone, "");
        }

        private Wall CreateWall(string coordinates)
        {
            return new Wall
            {
                WallPositionField1 = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                WallPositionField2 = (int.Parse(coordinates[2].ToString()), int.Parse(coordinates[3].ToString()))
            };
        }
        
        private void AddBerryToBoard(string instruction, int boardSize)
        {
            var validationResult = _validatorBerry.ValidateBerryInputWithReason(instruction, boardSize);
            if (!validationResult.IsValid)
            {
                _eventHandler.PublishEvent(EventType.BerryCreationError, $"{validationResult.Reason}");
                return;
            }
            
            var stringBuilder = new StringBuilder();
            foreach (var c in instruction.Where(c => c != ' '))
                stringBuilder.Append(c);
            var berryToBuild = stringBuilder.ToString().Remove(0, 1).Split("B");
            foreach (var coordinates in berryToBuild)
                _board.CreateBerryOnBoard(CreateBerry(coordinates, new BlueBerry()));

            _eventHandler.PublishEvent(EventType.BerryCreationDone, "");
        }

        private IBerry CreateBerry<T>(string coordinates, T berryType) where T : IBerry, new()
        {
            return new T
            {
                BerryPosition = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                IsEaten = false
            };
        }
    }
}