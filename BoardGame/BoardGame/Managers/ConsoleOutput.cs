using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class ConsoleOutput : IPresentation
    {
        public void GenerateOutput(IGameBoard board, IReadOnlyList<IPiece> pieces)
        {
            Console.WriteLine("=====");
            Console.WriteLine($"Player(s): {pieces.Count}");
            Console.WriteLine("=====");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (board.Board[j, i].IsTaken)
                        str.Append(board.Board[j, i].TakenBy.PieceId);
                    else
                        str.Append("-");
                }
                Console.WriteLine(str.ToString());
            }
        }
    }
}