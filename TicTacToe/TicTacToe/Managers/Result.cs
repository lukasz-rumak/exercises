using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Managers
{
    public class Result : IResult
    {
        private readonly int _size;

        public Result()
        {
            _size = 3;
        }

        public Player CheckForWinner(Field[,] board)
        {
            var winner = Player.None;
            winner = CheckHorizontal(board);
            if (winner != Player.None)
                return winner;
            winner = CheckVertical(board);
            if (winner != Player.None)
                return winner;
            winner = CheckCross(board);
            if (winner != Player.None)
                return winner;
            return winner;
        }

        private Player CheckHorizontal(Field[,] board)
        {
            for (int i = 0; i < _size; i++)
            {
                var counter = 0;
                for (int j = 0; j < _size; j++)
                {
                    if (board[i, j].Player == Player.O)
                        counter += 1;
                    else if (board[i, j].Player == Player.X)
                        counter -= 1;
                    else
                        break;
                }

                if (counter == 3)
                    return Player.O;
                if (counter == -3)
                    return Player.X;
            }

            return Player.None;
        }

        private Player CheckVertical(Field[,] board)
        {
            for (int i = 0; i < _size; i++)
            {
                var counter = 0;
                for (int j = 0; j < _size; j++)
                {
                    if (board[j, i].Player == Player.O)
                        counter += 1;
                    else if (board[j, i].Player == Player.X)
                        counter -= 1;
                    else
                        break;
                }

                if (counter == 3)
                    return Player.O;
                if (counter == -3)
                    return Player.X;
            }

            return Player.None;
        }

        private Player CheckCross(Field[,] board)
        {
            var counter = 0;
            for (int i = 0; i < _size; i++)
            {
                if (board[i, i].Player == Player.O)
                    counter += 1;
                else if (board[i, i].Player == Player.X)
                    counter -= 1;
                else
                    break;
            }

            if (counter == 3)
                return Player.O;
            if (counter == -3)
                return Player.X;

            counter = 0;
            var counterX = 2;
            var counterY = 0;
            for (int i = 0; i < _size; i++)
            {
                if (board[counterX, counterY].Player == Player.O)
                    counter += 1;
                else if (board[counterX, counterY].Player == Player.X)
                    counter -= 1;
                else
                    break;

                counterX -= 1;
                counterY += 1;
            }

            switch (counter)
            {
                case 3:
                    return Player.O;
                case -3:
                    return Player.X;
                default:
                    return Player.None;
            }
        }
    }
}