using System;

namespace BoardGameApi.Models
{
    public class AddPlayer
    {
        public Guid SessionId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerType { get; set; } // TODO
        public string StartPosition { get; set; }
    }
}