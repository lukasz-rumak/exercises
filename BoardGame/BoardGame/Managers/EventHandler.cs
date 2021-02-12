using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class EventHandler : IEvent
    {
        public IDictionary<EventType, Action<string>> Events { get; set; }
        public List<string> EventLog { get; set; }
        
        private readonly IPresentation _presentation;
                
        public EventHandler(IPresentation presentation)
        {
            _presentation = presentation;
            Events = CreateEvents();
            EventLog = new List<string>();
        }

        private Dictionary<EventType, Action<string>> CreateEvents()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.PieceMove] = EventPieceMove,
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
            EventLog.Add(eventMsg);
        }
        
        private void EventWallCreationError(string description)
        {
            var eventMsg = $"Event: The wall(s) were not created! {description}";
            _presentation.PrintEventOutput(EventType.WallCreationError, eventMsg);
            EventLog.Add(eventMsg);
        }
        
        private void EventOutsideBoundaries(string description)
        {
            var eventMsg = $"Event: move not possible (outside of the boundaries)! {description}";
            _presentation.PrintEventOutput(EventType.OutsideBoundaries, eventMsg);
            EventLog.Add(eventMsg);
        }
        
        private void EventFieldTaken(string description)
        {
            var eventMsg = $"Event: move not possible (field already taken)! {description}";
            _presentation.PrintEventOutput(EventType.FieldTaken, eventMsg);
            EventLog.Add(eventMsg);
        }
        
        private void EventWallOnTheRoute(string description)
        {
            var eventMsg = $"Event: move not possible (wall on the route)! {description}";
            _presentation.PrintEventOutput(EventType.WallOnTheRoute, eventMsg);
            EventLog.Add(eventMsg);
        }
        
        private void EventNone(string description)
        {
            var eventMsg = $"Event: none! {description}";
            _presentation.PrintEventOutput(EventType.None, eventMsg);
            EventLog.Add(eventMsg);
        }
    }
}