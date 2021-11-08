using System;
using System.Collections.Generic;
using Draughts.Interfaces;
using Draughts.Models;
using Action = Draughts.Models.Action;

namespace Draughts.Managers
{
    public class MovementHandler : IMovement
    {
        private readonly Dictionary<Action, Action<Dictionary<(int, int), Field>, Players, Event>> _actions;
        public MovementHandler()
        {
            _actions = CreateActions();
        }

        public void MovePawn(Dictionary<(int, int), Field> board, Players player, List<Event> events)
        {
            foreach (var e in events)
            {
                _actions[e.Action](board, player, e);
            }
        }

        private Dictionary<Action, Action<Dictionary<(int, int), Field>, Players, Event>> CreateActions()
        {
            return new Dictionary<Action, Action<Dictionary<(int, int), Field>, Players, Event>>
            {
                [Action.Move] = ActionMove,
                [Action.Kill] = ActionKill
            };
        }

        private void ActionMove(Dictionary<(int, int), Field> board, Players player, Event @event)
        {
            board[@event.Source].Player = Players.None;
            board[@event.Destination].Player = player;
        }
        
        private void ActionKill(Dictionary<(int, int), Field> board, Players player, Event @event)
        {
            board[@event.Source].Player = Players.None;
            board[@event.Destination].Player = Players.None;
        }
    }
}


        