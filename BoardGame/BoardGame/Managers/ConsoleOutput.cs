using System;
using System.Collections.Generic;
using System.Text;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class ConsoleOutput : IPresentation
    {
        public Dictionary<EventType, Action<string>> EventsOutput { get; set; }

        public ConsoleOutput()
        {
            EventsOutput = CreateEventsOutput();
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

        private Dictionary<EventType, Action<string>> CreateEventsOutput()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.PieceMove] = description => 
                    Console.WriteLine($"Event: {description}!"),
                [EventType.WallCreationError] = description =>
                    Console.WriteLine($"Event: The wall(s) were not created! {description}"),
                [EventType.OutsideBoundaries] = description =>
                    Console.WriteLine($"Event: move not possible (outside of the boundaries)! {description}"),
                [EventType.FieldTaken] = description =>
                    Console.WriteLine($"Event: move not possible (field already taken)! {description}"),
                [EventType.WallOnTheRoute] = description =>
                    Console.WriteLine($"Event: move not possible (wall on the route)! {description}"),
                [EventType.None] = description => Console.WriteLine($"Event: none! {description}")
            };
        }
    }
}