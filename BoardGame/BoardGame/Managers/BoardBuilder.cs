using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        private IGameBoard _board;
        
        public BoardBuilder()
        {
            _board = new GameBoard(new EventHandler(new ConsoleOutput()), new Validator());
        }

        public IBoardBuilder WithSize(int size)
        {
            _board.WithSize = size;
            return this;
        }

        public IBoardBuilder AddWall(string instruction)
        {
            _board.AddWallsToBoard(instruction);
            return this;
        }

        public IGameBoard BuildBoard()
        {
            _board.Board = _board.GenerateBoard(_board.WithSize);
            return _board;
        }
    }
}