using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class Validator : IValidator
    {
        public Validator()
        {
        }

        public bool ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            
            foreach (var character in input)
            {
                if (!(character == 'M' || character == 'R' || character == 'L'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}