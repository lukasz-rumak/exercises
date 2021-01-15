using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class BoardBuilder : IBoardBuilder
    {
        public Field[,] Board { get; }
        public int WithSize { get; }

        public BoardBuilder(int size)
        {
            Board = GenerateBoard(size);
            WithSize = size;
        }
        
        private Field[,] GenerateBoard(int size)
        {
            var board = new Field[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
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