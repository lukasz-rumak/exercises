﻿using System;
using System.Text;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Managers
{
    public class Output : IOutput
    {
        public void ShowCurrentBoardStatus(Field[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                var stringBuilder = new StringBuilder().Append("| ");
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Player == Player.None)
                        stringBuilder.Append("-");
                    else
                        stringBuilder.Append(board[i, j].Player);
                    stringBuilder.Append(" | ");
                }

                Console.WriteLine(stringBuilder.ToString());
            }
        }
    }
}