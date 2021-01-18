using System;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Managers
{
    public class Game : IGame
    {
        private BuildBoard _board;
        private readonly IOutput _output;
        private readonly IResult _result;
        private readonly IValidator _validator;

        public Game(IOutput output, IResult result, IValidator validator)
        {
            _board = new BuildBoard();
            _output = output;
            _result = result;
            _validator = validator;
        }

        public void PlayGameInConsole()
        {
            var winner = Player.None;
            _output.ShowCurrentBoardStatus(_board.Board);
            for (int i = 0; i < 9; i++)
            {
                PlayerMove(i % 2 == 0 ? Player.O : Player.X);
                _output.ShowCurrentBoardStatus(_board.Board);
                if (i > 3)
                    winner = _result.CheckForWinner(_board.Board);
                if (winner != Player.None)
                    break;
            }

            Console.WriteLine(winner != Player.None ? $"Player {winner} won!" : "Dead-heat!");
        }
        
        private void PlayerMove(Player player)
        {
            while (true)
            {
                Console.WriteLine($"Player {player} move: ");
                var input = Console.ReadLine();
                if (_validator.ValidateInput(input))
                {
                    if (IsFieldTaken(input))
                        Console.WriteLine("Field already taken!");
                    else
                    {
                        UpdateBoard(player, input);
                        break;
                    }                    
                }
                else
                    Console.WriteLine("Please provide valid input!");
            }
        }

        private bool IsFieldTaken(string input)
        {
            if (_board.Board[int.Parse(input[0].ToString()), int.Parse(input[1].ToString())].Player == Player.None)
                return false;
            return true;
        }

        private void UpdateBoard(Player player, string input)
        {
            _board.Board[int.Parse(input[0].ToString()), int.Parse(input[1].ToString())].Player = player;
        }
    }
}