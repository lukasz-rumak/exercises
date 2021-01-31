using System.Linq;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Validator : IValidator
    {
        public bool ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(c => c == 'P' || c == 'M' || c == 'R' || c == 'L');
        }
    }
}