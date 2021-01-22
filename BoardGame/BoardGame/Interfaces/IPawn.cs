using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPawn
    {
        int PawnId { get; set; }
        bool IsAlive { get; set; }
        IPosition Position { get; }
        Direction ChangeDirectionToRight(Direction direction);
        Direction ChangeDirectionToLeft(Direction direction);
    }
}