using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class EventHandler : IEvent
    {
        public IDictionary<EventType, Action<string>> Events { get; set; }
        
        private readonly IPresentation _presentation;
                
        public EventHandler(IPresentation presentation)
        {
            _presentation = presentation;
            Events = CreateEvents();
        }

        private Dictionary<EventType, Action<string>> CreateEvents()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.WallCreationError] = _presentation.EventsOutput[EventType.WallCreationError],
                [EventType.OutsideBoundaries] = _presentation.EventsOutput[EventType.OutsideBoundaries],
                [EventType.FieldTaken] = _presentation.EventsOutput[EventType.FieldTaken],
                [EventType.WallOnTheRoute] = _presentation.EventsOutput[EventType.WallOnTheRoute],
                [EventType.None] = _presentation.EventsOutput[EventType.None]
            };
        }
    }
}