using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IEventHandler
    {
        List<EventLog> EventsLog { get; set; }
        void PublishEvent(EventType eventType, string description);
    }
}