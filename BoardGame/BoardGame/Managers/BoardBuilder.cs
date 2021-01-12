using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        private readonly int _size;
//        public Field[,] Board { get; set; }
//        public int WithSize { get ; set; }

        public BoardBuilder(int size)
        {
            _size = size;
//            Board = GenerateBoard();
//            WithSize = size;
        }

        public Field[,] GenerateBoard()
        {
            var board = new Field[_size, _size];
            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                    board[i, j] = new Field(i, j);
            return board;
        }
    }
}

//        public List<Field> GenerateBoardObsolete()
//        {
//            var board = new List<Field>();
//            for (int i = 0; i < _size; i++)
//            for (int j = 0; j < _size; j++)
//                board.Add(new Field(j, i));
//            return board;
//        }