using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class ConsoleOutput : IPresentation
    {
        private readonly Dictionary<EventType, Action<string>> _eventsOutput;

        public ConsoleOutput()
        {
            _eventsOutput = CreateEventsOutput();
        }
        
        public void GenerateOutput(IGameBoard board, IReadOnlyList<IPiece> pieces)
        {
            Console.WriteLine("=====");
            Console.WriteLine($"Player(s): {pieces.Count}");
            Console.WriteLine("=====");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (board.Board[j, i].IsTaken)
                        str.Append(board.Board[j, i].TakenBy.PieceId);
                    else
                        str.Append("-");
                }
                Console.WriteLine(str.ToString());
            }
        }

        public void PrintEventOutput(EventType eventType, string description)
        {
            _eventsOutput[eventType](description);
        }
        
        private Dictionary<EventType, Action<string>> CreateEventsOutput()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.PieceMove] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallCreationError] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.OutsideBoundaries] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.FieldTaken] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallOnTheRoute] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.None] = eventMsg => 
                    Console.WriteLine($"{eventMsg}")
            };
        }
    }
}