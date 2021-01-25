using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IGameBoard _board;
        private readonly IPresentation _present;
        
        public GameMaster(IValidator validator, IGameBoard board, IPresentation present)
        {
            _validator = validator;
            _board = board;
            _present = present;
        }

        public string[] PlayTheGame(string[] instructions)
        {
            if (instructions is null)
                return new []{"Instruction not clear. Exiting..."};
            
            var pawns = CreatePawns(instructions);
            ExecuteValidation(pawns, instructions);
            ExecuteTheInstructions(pawns, instructions);
            return GetResult(pawns);
        }
        
        private List<Pawn> CreatePawns(IReadOnlyCollection<string> instructions)
        {
            var pawns = new List<Pawn>();
            for (var i = 0; i < instructions.Count; i++)
            {
                if (_board.WithSize > i)
                {
                    pawns.Add(new Pawn(i));
                    _board.Board[i, i].TakenBy = pawns[i];    
                }
            }
            return pawns;
        }
        
        private void ExecuteValidation(IReadOnlyList<Pawn> pawns, IReadOnlyList<string> instructions)
        {
            for (var i = 0; i < pawns.Count; i++)
                if (!_validator.ValidateInput(instructions[i]))
                    pawns[i].IsAlive = false;
        }
        
        private void ExecuteTheInstructions(IReadOnlyList<Pawn> pawns, IReadOnlyList<string> instructions)
        {
            var longestInstruction = GetTheLongestInstruction(pawns, instructions);
            
            for (var i = 0; i < longestInstruction; i++)
                for (var j = 0; j < pawns.Count; j++)
                    if (pawns[j].IsAlive)
                        if (instructions[j].Length > i)
                        {
                            _board.ExecuteThePlayerInstruction(pawns[j], instructions[j][i]);
                            _present.GenerateOutput(_board, pawns);
                        }
        }

        private int GetTheLongestInstruction(IReadOnlyCollection<Pawn> pawns, IReadOnlyList<string> instructions)
        {
            var longest = 0;
            for (var i = 0; i < pawns.Count; i++)
                if (instructions[i].Length > longest)
                    longest = instructions[i].Length;
            return longest;
        }
        
        private string[] GetResult(IReadOnlyList<Pawn> pawns)
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