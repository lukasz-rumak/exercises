using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class EventHandler : IEvent
    {
        public IDictionary<EventType, Action<string>> Events { get; set; }
        public List<EventLog> EventsLog { get; set; }
        
        private readonly IPresentation _presentation;
                
        public EventHandler(IPresentation presentation)
        {
            _presentation = presentation;
            Events = CreateEvents();
            EventsLog = new List<EventLog>();
        }

        private Dictionary<EventType, Action<string>> CreateEvents()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.PieceMove] = EventPieceMove,
                [EventType.WallCreationDone] = EventWallCreationDone,
                [EventType.WallCreationError] = EventWallCreationError,
                [EventType.OutsideBoundaries] = EventOutsideBoundaries,
                [EventType.FieldTaken] = EventFieldTaken,
                [EventType.WallOnTheRoute] = EventWallOnTheRoute,
                [EventType.None] = EventNone
            };
        }

        private void EventPieceMove(string description)
        {
            var eventMsg = $"Event: {description}!";
            _presentation.PrintEventOutput(EventType.PieceMove, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.PieceMove, Description = eventMsg});
        }
        
        private void EventWallCreationDone(string description)
        {
            var eventMsg = $"Event: The wall(s) have been created! {description}";
            _presentation.PrintEventOutput(EventType.WallCreationDone, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallCreationDone, Description = eventMsg});
        }
        
        private void EventWallCreationError(string description)
        {
            var eventMsg = $"Event: The wall(s) were not created! {description}";
            _presentation.PrintEventOutput(EventType.WallCreationError, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallCreationError, Description = eventMsg});
        }
        
        private void EventOutsideBoundaries(string description)
        {
            var eventMsg = $"Event: move not possible (outside of the boundaries)! {description}";
            _presentation.PrintEventOutput(EventType.OutsideBoundaries, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.OutsideBoundaries, Description = eventMsg});
        }
        
        private void EventFieldTaken(string description)
        {
            var eventMsg = $"Event: move not possible (field already taken)! {description}";
            _presentation.PrintEventOutput(EventType.FieldTaken, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.FieldTaken, Description = eventMsg});
        }
        
        private void EventWallOnTheRoute(string description)
        {
            var eventMsg = $"Event: move not possible (wall on the route)! {description}";
            _presentation.PrintEventOutput(EventType.WallOnTheRoute, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallOnTheRoute, Description = eventMsg});
        }
        
        private void EventNone(string description)
        {
            var eventMsg = $"Event: none! {description}";
            _presentation.PrintEventOutput(EventType.None, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.None, Description = eventMsg});
        }
    }
}