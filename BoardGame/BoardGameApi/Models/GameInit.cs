using System;

namespace BoardGameApi.Models
{
    public class GameInit
    {
        public Guid SessionId { get; set; }
        public Board Board { get; set; }
    }
}