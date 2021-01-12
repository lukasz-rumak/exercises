using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameManager : IGame
    {
        private readonly IValidator _validator;
        private readonly Field[,] _board;
        private readonly IPlayer _pawn;
        
        public GameManager(IValidator validator, IBoardBuilder boardBuilder, IPlayer pawn)
        {
            _validator = validator;
            _board = boardBuilder.GenerateBoard();
            _pawn = pawn;
        }
        
        public string PlayTheGame(string input)
        {
            if (!_validator.ValidateInput(input))
                return "Instruction not clear. Exiting...";

            var x = 0;
            var y = 0;
            var direction = Direction.North;
            
            foreach (var instruction in input)
            {
                if (instruction == 'M')
                {
                    (x, y) = _pawn.MakeMove(direction, x, y);
                }
                else
                {
                    direction = instruction == 'R' ? _pawn.ChangeDirectionToRight(direction) : _pawn.ChangeDirectionToLeft(direction);
                }
            }
            
            return ConvertResult(_board, direction, x, y);
        }

        private string ConvertResult(Field[,] board, Direction direction, int x, int y)
        {
            return new StringBuilder().Append(board[x, y].X)
                .Append(" ")
                .Append(board[x, y].Y)
                .Append(" ")
                .Append(direction)
                .ToString();
        }
    }
}