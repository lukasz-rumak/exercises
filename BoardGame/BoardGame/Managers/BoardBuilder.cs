using System.Linq;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        private readonly IGameBoard _board;
        private readonly IValidatorWall _validator;
        private readonly IEvent _eventHandler;

        public BoardBuilder(IEvent eventHandler, IValidatorWall validatorWall)
        {
            _eventHandler = eventHandler;
            _validator = validatorWall;
            _board = new GameBoard(_eventHandler);
        }

        public IBoardBuilder WithSize(int size)
        {
            _board.WithSize = size;
            return this;
        }

        public IBoardBuilder AddWall(string instruction)
        {
            AddWallToBoard(instruction, _board.WithSize);
            return this;
        }

        public IGameBoard BuildBoard()
        {
            _board.GenerateBoard(_board.WithSize);
            return _board;
        }

        private void AddWallToBoard(string instruction, int boardSize)
        {
            var validationResult = _validator.ValidateWallInputWithReason(instruction, boardSize);
            if (!validationResult.IsValid)
            {
                _eventHandler.Events[EventType.WallCreationError]($"{validationResult.Reason}");
                return;
            }

            var stringBuilder = new StringBuilder();
            foreach (var c in instruction.Where(c => c != ' '))
                stringBuilder.Append(c);
            var wallsToBuild = stringBuilder.ToString().Remove(0, 1).Split("W");
            foreach (var coordinates in wallsToBuild)
            {
                _board.CreateWallOnBoard(CreateWall(coordinates));
            }
        }

        private Wall CreateWall(string coordinates)
        {
            return new Wall
            {
                WallPositionField1 = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                WallPositionField2 = (int.Parse(coordinates[2].ToString()), int.Parse(coordinates[3].ToString()))
            };
        }
    }
}