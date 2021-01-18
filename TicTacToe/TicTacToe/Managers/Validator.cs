using TicTacToe.Interfaces;

namespace TicTacToe.Managers
{
    public class Validator : IValidator
    {
        public bool ValidateInput(string input)
        {
            if (input.Length != 2)
                return false;
            if (!int.TryParse(input[0].ToString(), out var result0))
                return false;
            if (!int.TryParse(input[1].ToString(), out var result1))
                return false;
            if (int.Parse(input[0].ToString()) < 0 || int.Parse(input[0].ToString()) > 2)
                return false;
            if (int.Parse(input[1].ToString()) < 0 || int.Parse(input[1].ToString()) > 2)
                return false;
            return true;
        }
    }
}