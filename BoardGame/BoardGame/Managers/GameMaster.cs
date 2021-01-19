﻿using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class GameMaster : IGame
    {
        private readonly IValidator _validator;
        private readonly IGameBoard _board;
        
        public GameMaster(IValidator validator, IGameBoard board)
        {
            _validator = validator;
            _board = board;
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
                pawns.Add(new Pawn(_board, i));
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
                            pawns[j].ExecuteThePlayerInstruction(instructions[j][i]);
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