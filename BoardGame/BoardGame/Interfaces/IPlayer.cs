using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPlayer
    {
        (int, int) MakeMove(Direction direction, int x, int y);
        Direction ChangeDirectionToRight(Direction direction);
        Direction ChangeDirectionToLeft(Direction direction);
    }
}