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
            foreach (var piece in pieces)
                Console.WriteLine($"Player {piece.PieceId}: {piece.CalculateScore()} point(s) collected");
            Console.WriteLine("=====");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (board.IsFieldTaken(j, i))
                        str.Append(board.ReturnPieceIdFromTakenField(j, i));
                    else if (board.IsNotEatenBerryOnField(j, i))
                        str.Append("b");
                    else
                        str.Append("-");
                }
                Console.WriteLine(str.ToString());
            }
        }
        
        public string GenerateOutputApi(IGameBoard board, IReadOnlyList<IPiece> pieces)
        {
            var result = new StringBuilder();
            foreach (var piece in pieces)
                result.Append($"Player {piece.PieceId}: {piece.CalculateScore()} point(s) collected; ");
            for (int i = board.WithSize - 1; i >= 0; i--)
            {
                var str = new StringBuilder();
                for (int j = 0; j < board.WithSize; j++)
                {
                    if (board.IsFieldTaken(j, i))
                        str.Append(board.ReturnPieceIdFromTakenField(j, i));
                    else if (board.IsNotEatenBerryOnField(j, i))
                        str.Append("b");
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
                [EventType.GameStarted] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.BoardCreationDone] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.BoardCreationError] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.PlayerAdded] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.PieceMoved] = eventMsg => 
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallCreationDone] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallCreationError] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.BerryCreationDone] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.BerryCreationError] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.BerryEaten] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.OutsideBoundaries] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.FieldTaken] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.WallOnTheRoute] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.IncorrectPlayerId] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.GeneratedBoardOutput] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.IncorrectBoardSize] = eventMsg =>
                    Console.WriteLine($"{eventMsg}"),
                [EventType.None] = eventMsg => 
                    Console.WriteLine($"{eventMsg}")
            };
        }
    }
}