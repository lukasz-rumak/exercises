using System;

namespace BoardGameApi.Models
{
    public class MovePlayer
    {
        public Guid SessionId { get; set; }
        public int PlayerId { get; set; }
        public string Move { get; set; }
    }
}