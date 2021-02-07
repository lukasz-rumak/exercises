using System;
using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IEvent
    {
        IDictionary<EventType, Action<string>> Events { get; set; } 
    }
}