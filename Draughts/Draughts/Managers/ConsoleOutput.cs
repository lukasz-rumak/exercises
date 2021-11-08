using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draughts.Interfaces;
using Draughts.Models;
using Action = Draughts.Models.Action;

namespace Draughts.Managers
{
    public class ConsoleOutput : IOutput
    {
        public void GenerateBoardVisualization(IBoardCreator board)
        {
            var firstLine = new StringBuilder();
            firstLine.Append(" |");
            for (int i = 0; i < board.GetBoardSize(); i++)
            {
                firstLine.Append(i);
            }
            Console.WriteLine($"{firstLine}");
            for (int y = 0; y < board.GetBoardSize(); y++)
            {
                var output = new StringBuilder();
                output.Append($"{y}|");
                for (int x = 0; x < board.GetBoardSize(); x++)
                {
                    if (board.GetBoard()[(x, y)].Player == Players.None)
                        output.Append('x');
                    else if (board.GetBoard()[(x, y)].Player == Players.Player1)
                        output.Append('1');
                    else if (board.GetBoard()[(x, y)].Player == Players.Player2)
                        output.Append('2');
                }
                Console.WriteLine($"{output}");
            }
        }

        public void GeneratePlayerMovement(Players player, List<Event> events)
        {
            foreach (var e in events)
            {
                Console.WriteLine(e.Action == Action.Kill
                    ? $"{player} killed pawn at {e.Destination}"
                    : $"{player} moved to {e.Destination}");
            }
        }

        public void GenerateSummary(IBoardCreator board)
        {
            Console.WriteLine(
                $"Player1: {board.GetBoard().Count(x => x.Value.Player == Players.Player1)} Player2: {board.GetBoard().Count(x => x.Value.Player == Players.Player2)}");
        }
    }
}