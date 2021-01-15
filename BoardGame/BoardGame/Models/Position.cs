using BoardGame.Interfaces;

namespace BoardGame.Models
{
    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
    }
}