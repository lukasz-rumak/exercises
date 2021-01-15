using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
        Direction Direction { get; set; }
    }
}