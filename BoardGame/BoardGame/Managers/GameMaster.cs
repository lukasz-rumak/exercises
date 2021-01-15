using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IBoardBuilder _board;
        
        public GameMaster(IValidator validator, IBoardBuilder boardBuilder)
        {
            _validator = validator;
            _board = boardBuilder;
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};
            
            var pawns = new List<Pawn>();
            for (var i = 0; i < instructions.Length; i++)
                pawns.Add(new Pawn(_validator, _board, i));
            
            for (var i = 0; i < instructions.Length; i++)
                pawns[i].ExecuteThePlayerInstruction(instructions[i]);

            return GetResult(pawns);
        }

        private string[] GetResult(List<Pawn> pawns)
        {
            var result = new string[pawns.Count];
            for (var i = 0; i < pawns.Count; i++)
            {
                result[i] = pawns[i].IsAlive
                    ? new StringBuilder().Append(pawns[i].Position.X).Append(" ").Append(pawns[i].Position.Y)
                        .Append(" ").Append(pawns[i].Position.Direction).ToString()
                    : result[i] = @"Instruction not clear. Exiting...";
            }
            
            return result;
        }
    }
}