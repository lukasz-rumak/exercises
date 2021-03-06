using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class EventHandler : IEventHandler
    {
        public List<EventLog> EventsLog { get; set; }

        private readonly IPresentation _presentation;
        private readonly IDictionary<EventType, Action<string>> _events;
        
        public EventHandler(IPresentation presentation)
        {
            _presentation = presentation;
            _events = CreateEvents();
            EventsLog = new List<EventLog>();
        }

        public void PublishEvent(EventType eventType, string description)
        {
            _events[eventType](description);
        }

        private Dictionary<EventType, Action<string>> CreateEvents()
        {
            return new Dictionary<EventType, Action<string>>
            {
                [EventType.GameStarted] = EventGameStarted,
                [EventType.BoardCreationDone] = EventBoardCreationDone,
                [EventType.BoardCreationError] = EventBoardCreationError,
                [EventType.PlayerAdded] = EventPlayerAdded,
                [EventType.PieceMoved] = EventPieceMove,
                [EventType.WallCreationDone] = EventWallCreationDone,
                [EventType.WallCreationError] = EventWallCreationError,
                [EventType.BerryCreationDone] = EventBerryCreationDone,
                [EventType.BerryCreationError] = EventBerryCreationError,
                [EventType.BerryEaten] = EventBerryEaten,
                [EventType.OutsideBoundaries] = EventOutsideBoundaries,
                [EventType.FieldTaken] = EventFieldTaken,
                [EventType.WallOnTheRoute] = EventWallOnTheRoute,
                [EventType.IncorrectPlayerId] = EventIncorrectPlayerId,
                [EventType.GeneratedBoardOutput] = EventGeneratedBoardOutput,
                [EventType.IncorrectBoardSize] = EventIncorrectBoardSize,
                [EventType.None] = EventNone
            };
        }
        
        private void EventGameStarted(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.GameStarted, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.GameStarted, Description = eventMsg});
        }
        
        private void EventPlayerAdded(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.PlayerAdded, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.PlayerAdded, Description = eventMsg});
        }
        
        private void EventBoardCreationDone(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.BoardCreationDone, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.BoardCreationDone, Description = eventMsg});
        }
        
        private void EventBoardCreationError(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.BoardCreationError, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.BoardCreationError, Description = eventMsg});
        }

        private void EventPieceMove(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.PieceMoved, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.PieceMoved, Description = eventMsg});
        }
        
        private void EventWallCreationDone(string description)
        {
            var eventMsg = CreateEventMessage($"The wall has been created! {description}");
            _presentation.PrintEventOutput(EventType.WallCreationDone, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallCreationDone, Description = eventMsg});
        }
        
        private void EventWallCreationError(string description)
        {
            var eventMsg = CreateEventMessage($"The wall was not created! {description}");
            _presentation.PrintEventOutput(EventType.WallCreationError, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallCreationError, Description = eventMsg});
        }
        
        private void EventBerryCreationDone(string description)
        {
            var eventMsg = CreateEventMessage($"The berry has been created! {description}");
            _presentation.PrintEventOutput(EventType.BerryCreationDone, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.BerryCreationDone, Description = eventMsg});
        }
        
        private void EventBerryCreationError(string description)
        {
            var eventMsg = CreateEventMessage($"The berry was not created! {description}");
            _presentation.PrintEventOutput(EventType.BerryCreationError, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.BerryCreationError, Description = eventMsg});
        }
        
        private void EventBerryEaten(string description)
        {
            var eventMsg = CreateEventMessage($"The piece moved and the berry has been eaten. {description}");
            _presentation.PrintEventOutput(EventType.BerryEaten, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.BerryEaten, Description = eventMsg});
        }
        
        private void EventOutsideBoundaries(string description)
        {
            var eventMsg = CreateEventMessage($"Move not possible (outside of the boundaries)! {description}");
            _presentation.PrintEventOutput(EventType.OutsideBoundaries, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.OutsideBoundaries, Description = eventMsg});
        }
        
        private void EventFieldTaken(string description)
        {
            var eventMsg = CreateEventMessage($"Move not possible (field already taken)! {description}");
            _presentation.PrintEventOutput(EventType.FieldTaken, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.FieldTaken, Description = eventMsg});
        }
        
        private void EventWallOnTheRoute(string description)
        {
            var eventMsg = CreateEventMessage($"Move not possible (wall on the route)! {description}");
            _presentation.PrintEventOutput(EventType.WallOnTheRoute, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.WallOnTheRoute, Description = eventMsg});
        }
        
        private void EventIncorrectPlayerId(string description)
        {
            var eventMsg = CreateEventMessage($"Incorrect player id! {description}");
            _presentation.PrintEventOutput(EventType.IncorrectPlayerId, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.IncorrectPlayerId, Description = eventMsg});
        }
        
        private void EventGeneratedBoardOutput(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.GeneratedBoardOutput, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.GeneratedBoardOutput, Description = eventMsg});
        }
        
        private void EventIncorrectBoardSize(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.IncorrectBoardSize, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.IncorrectBoardSize, Description = eventMsg});
        }
        
        private void EventNone(string description)
        {
            var eventMsg = CreateEventMessage(description);
            _presentation.PrintEventOutput(EventType.None, eventMsg);
            EventsLog.Add(new EventLog {Type = EventType.None, Description = eventMsg});
        }

        private string CreateEventMessage(string description)
        {
            string eventMsg = null;
            if (!string.IsNullOrWhiteSpace(description))
                eventMsg = description;
            return eventMsg;
        }
    }
}