using System;

namespace BoardGameApi.Models
{
    public class Wall
    {
        public Guid SessionId { get; set; }
        public string WallCoordinates { get; set; }
    }
}