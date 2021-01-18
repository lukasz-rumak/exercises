using TicTacToe.Models;

namespace TicTacToe.Managers
{
    public class BuildBoard
    {
        public Field[,] Board { get; set; }

        public BuildBoard()
        {
            Board = GenerateBoard();
        }

        private Field[,] GenerateBoard()
        {
            var board = new Field[3, 3];
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = new Field
                    {
                        Player = Player.None,
                        X = i,
                        Y = j
                    };
                }
            }

            return board;
        }
    }
}