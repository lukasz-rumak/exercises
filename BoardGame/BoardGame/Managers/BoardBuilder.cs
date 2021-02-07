using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        private IGameBoard _board;
        
        public BoardBuilder()
        {
            _board = new GameBoard(new EventHandler(new ConsoleOutput()));
        }

        public IBoardBuilder WithSize(int size)
        {
            _board.WithSize = size;
            return this;
        }

        public IGameBoard BuildBoard()
        {
            _board.Board = _board.GenerateBoard(_board.WithSize);
            _board.Walls = new List<Wall>();
            return _board;
        }
    }
}