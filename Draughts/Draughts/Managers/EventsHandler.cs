using System;
using System.Collections.Generic;
using System.Linq;
using Draughts.Interfaces;
using Draughts.Models;
using Action = Draughts.Models.Action;

namespace Draughts.Managers
{
    public class EventsHandler : IEventsHandler
    {
        private readonly IBoardCreator _board;
        private readonly IEventsCreator _eventsCreator; 

        public EventsHandler(IBoardCreator board, IEventsCreator eventsCreator)
        {
            _board = board;
            _eventsCreator = eventsCreator;
        }
        
        public List<Event> ReturnTheBestEventsToExecute(Players player)
        {
            var allEvents = CreateAllEventsForGivenPlayer(player);

            return ReturnTheLongestPath(allEvents);
        }

        public List<List<Event>> CreateAllEventsForGivenPlayer(Players player)
        {
            var allEvents = new List<List<Event>>();
            foreach (var b in _board.GetBoard().Where(b => b.Value.Player == player))
            {
                var x = b.Key.Item1;
                var y = b.Key.Item2;

                var events = _eventsCreator.CreateEventsForPawn(player, (x, y));
                allEvents.Add(SelectTheBestPathForPawn(events));
            }

            return allEvents;
        }

        public List<List<Event>> CreateAllEventsForGivenPawn(Players player, int pawnPositionX, int pawnPositionY)
        {
            var events = _eventsCreator.CreateEventsForPawn(player, (pawnPositionX, pawnPositionY));

            if (events == null || events.Count == 0)
                return new List<List<Event>>();

            return events.All(e => e.Action != Action.Kill) 
                ? events.Select(e => new List<Event> { e }).ToList() 
                : GroupAllPossiblePathsIfThereIsKilling(events);
        }

        public List<Event> SelectTheBestPathForPawn(List<Event> events)
        {
            if (events == null || events.Count == 0)
                return new List<Event>();
            
            var randomMoveEvent = ReturnRandomMoveEventIfThereIsNoKilling(events);
            if (randomMoveEvent != null)
                return randomMoveEvent;
            var allPaths= GroupAllPossiblePathsIfThereIsKilling(events);
            return ReturnTheLongestPath(allPaths);
        }
        
        private List<Event> ReturnRandomMoveEventIfThereIsNoKilling(List<Event> events)
        {
            if (events.Any(e => e.Action == Action.Kill))
                return null;
            var randomEvent = events[new Random().Next(0, events.Count)];
            return new List<Event> { randomEvent };
        }
        
        private List<List<Event>> GroupAllPossiblePathsIfThereIsKilling(List<Event> events)
        {
            var allPaths = new List<List<Event>>();
            if (events == null || events.Count == 0)
                return allPaths;
            
            while (events.Count > 0)
            {
                allPaths.Add(new List<Event>());
                allPaths[^1].Add(events[0]);
                for (var i = 1; i < events.Count; i++)
                {
                    if (Math.Abs(events[i].Destination.Item1 - events[i - 1].Destination.Item1) == 1
                        && Math.Abs(events[i].Destination.Item2 - events[i - 1].Destination.Item2) == 1)
                    {
                        allPaths[^1].Add(events[i]);
                    }
                    else
                        break;
                }

                events.RemoveRange(0, allPaths[^1].Count);
            }

            return allPaths;
        }

        private List<Event> ReturnTheLongestPath(List<List<Event>> allPaths)
        {
            var longestPath = new List<Event>();

            foreach (var path in allPaths)
            {
                if (longestPath.Count < path.Count)
                    longestPath = path;
                else if (longestPath.Count == path.Count)
                    longestPath = new Random().Next(0, 2) == 0 ? longestPath : path;
            }

            return longestPath;
        }

        /*public void SearchAlgorithm(Node node)
        {
            var pointer = new Pointer
            {
                Node = node,
                BeenThere = new List<(int, int)> { (node.PosX, node.PosY) }
            };
            while (true)
            {
                if (pointer.Node.LeftNode != null
                && !pointer.BeenThere.Contains((pointer.Node.LeftNode.PosX, pointer.Node.RightNode.PosY)))
                {
                    pointer.Node = pointer.Node.LeftNode;
                    pointer.BeenThere.Add((pointer.Node.PosX, pointer.Node.PosY));
                }
                else if (pointer.Node.RightNode != null
                && !pointer.BeenThere.Contains((pointer.Node.RightNode.PosX, pointer.Node.RightNode.PosY)))
                {
                    pointer.Node = pointer.Node.RightNode;
                    pointer.BeenThere.Add((pointer.Node.PosX, pointer.Node.PosY));
                }
                else
                {
                    if (pointer.Node.PreviousNode == null)
                        break;
                    pointer.Node = pointer.Node.PreviousNode;
                }
            }
        }*/
    }
}