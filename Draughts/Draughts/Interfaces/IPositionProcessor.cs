using Draughts.Models;

namespace Draughts.Interfaces
{
    public interface IPositionProcessor
    {
        Positions ReturnPositions(Players player, Direction direction, int currentX, int currentY);
    }
}