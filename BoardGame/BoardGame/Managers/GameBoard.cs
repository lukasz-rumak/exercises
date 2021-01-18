using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameBoard : IGameBoard
    {
        public Field[,] Board { get; set; }
        public int WithSize { get; set; }

        public Field[,] GenerateBoard()
        {
            var board = new Field[WithSize, WithSize];
            for (int i = 0; i < WithSize; i++)
            for (int j = 0; j < WithSize; j++)
                board[i, j] = new Field(i, j);
            return board;
        }
    }
}