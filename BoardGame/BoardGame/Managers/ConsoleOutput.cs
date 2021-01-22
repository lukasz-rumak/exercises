using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class ConsoleOutput : IPresentation
    {
        public void GenerateOutput(IGameBoard board, IReadOnlyList<Pawn> pawns)
        {
            Console.WriteLine("=====");
            Console.WriteLine($"Player(s): {pawns.Count}");
            Console.WriteLine("=====");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (i == 0 && j == 0)
                        str.Append("S");
                    else if (board.Board[j, i].IsTaken)
                        str.Append(board.Board[j, i].TakenBy.PawnId);
                    else
                        str.Append("-");
                }
                Console.WriteLine(str.ToString());
            }
        }
    }
}