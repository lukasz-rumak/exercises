using System.Collections.Generic;
using Draughts.Interfaces;
using Draughts.Models;

namespace Draughts.Managers
{
    public class EventsCreator : IEventsCreator
    {
        private readonly IBoardCreator _board;
        private readonly IPositionProcessor _positions;
        
        public EventsCreator(IBoardCreator board, IPositionProcessor positions)
        {
            _board = board;
            _positions = positions;
        }
        
        public List<Event> CreateEventsForPawn(Players player, (int x, int y) pos)
        {
            var node = new Node
            {
                PreviousNode = null,
                PosX = pos.x,
                PosY = pos.y,
            };

            var events = new List<Event>();
            var isAllNodeProcessed = false;

            while (!isAllNodeProcessed)
            {
                var positionsLeft = _positions.ReturnPositions(player, Direction.Left, node.PosX, node.PosY);
                var positionsRight = _positions.ReturnPositions(player, Direction.Right, node.PosX, node.PosY);

                if (node.PreviousNode == null)
                {
                    if (node.LeftNode == null)
                        ProcessInitialPosition(events, ref node, Direction.Left, player, positionsLeft);
                    else if (node.RightNode == null)
                        ProcessInitialPosition(events, ref node, Direction.Right, player, positionsRight);
                    else
                        isAllNodeProcessed = true;
                }
                else
                {
                    if (node.LeftNode == null)
                        ProcessKillingMove(events, ref node, Direction.Left, player, positionsLeft);
                    else if (node.RightNode == null)
                        ProcessKillingMove(events, ref node, Direction.Right, player, positionsRight);
                    else
                        node = node.PreviousNode;
                }
            }

            return events;
        }

        private void ProcessInitialPosition(List<Event> events, ref Node node, Direction direction, Players player, 
            Positions positions)
        {
            if (direction == Direction.Left)
                node.LeftNode = new EmptyNode();
            else node.RightNode = new EmptyNode();
            var isProcessed = ProcessMoveToEmptyField(events, ref node, direction, positions);
            if (!isProcessed)
                ProcessKillingMove(events, ref node, direction, player, positions);
        }

        private bool ProcessMoveToEmptyField(List<Event> events, ref Node node,
            Direction direction, Positions positions)
        {
            if (!ValidatePosition(positions.NearbyX, positions.NearbyY))
                return true;
            if (_board.GetBoard()[(positions.NearbyX, positions.NearbyY)].Player == Players.None)
            {
                events.Add(new Event
                    { Source = (node.PosX, node.PosY), Destination = (positions.NearbyX, positions.NearbyY), Action = Action.Move });
                var newNode = new Node
                {
                    PreviousNode = node,
                    PosX = positions.NearbyX,
                    PosY = positions.NearbyY
                };
                if (direction == Direction.Left)
                    node.LeftNode = newNode;
                else node.RightNode = newNode;

                return true;
            }

            return false;
        }

        private void ProcessKillingMove(List<Event> events, ref Node node,
            Direction direction, Players player, Positions positions)
        {
            if (direction == Direction.Left)
                node.LeftNode = new EmptyNode();
            else node.RightNode = new EmptyNode();
            var oppositePlayer = player == Players.Player1 ? Players.Player2 : Players.Player1;
            if (!ValidatePosition(positions.NearbyX, positions.NearbyY))
                return;
            if (!ValidatePosition(positions.DistantX, positions.DistantY))
                return;
            if (_board.GetBoard()[(positions.NearbyX, positions.NearbyY)].Player == oppositePlayer
                && _board.GetBoard()[(positions.DistantX, positions.DistantY)].Player == Players.None)
            {
                events.Add(new Event
                { Source = (node.PosX, node.PosY), Destination = (positions.NearbyX, positions.NearbyY), Action = Action.Kill });
                events.Add(new Event
                { Source = (node.PosX, node.PosY), Destination = (positions.DistantX, positions.DistantY), Action = Action.Move });
                var newNode = new Node
                {
                    PreviousNode = node,
                    PosX = positions.DistantX,
                    PosY = positions.DistantY
                };
                if (direction == Direction.Left)
                {
                    node.LeftNode = newNode;
                    node = node.LeftNode;
                }
                else
                {
                    node.RightNode = newNode;
                    node = node.RightNode;
                }
            }
        }
        
        private bool ValidatePosition(int x, int y)
        {
            var boardSize = _board.GetBoardSize();
            return x >= 0 && x < boardSize && y >= 0 && y < boardSize;
        }
    }
}