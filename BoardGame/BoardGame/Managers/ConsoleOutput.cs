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
                    if (board.IsFieldTaken(j, i))
                        str.Append(board.ReturnPieceIdFromTakenField(j, i));
                    else
                        str.Append("-");
                }
                Console.WriteLine(str.ToString());
            }
        }
        
        public string GenerateOutputApi(IGameBoard board, IReadOnlyList<IPiece> pieces)
        {
            var result = new StringBuilder();
            result.Append($"Player(s): {pieces.Count}");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (board.IsFieldTaken(j, i))
                        str.Append(board.ReturnPieceIdFromTakenField(j, i));
                    else
                        str.Append("-");
                }
                result.Append("|");
                result.Append(str);
            }

            return result.ToString();
        }

        public void PrintEventOutput(EventType eventType, string eventMsg)
        {
            _eventsOutput[eventType](eventMsg);
        }
        
        private Dictionary<EventType, Action<string>> CreateEventsOutput()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.BoardBuilt] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.PlayerAdded] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.PieceMoved] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallCreationDone] = eventMsg =>
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