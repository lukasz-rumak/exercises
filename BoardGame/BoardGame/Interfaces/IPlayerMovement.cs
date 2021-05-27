using System.Collections.Generic;

namespace BoardGame.Interfaces
{
    public interface IPlayerMovement
    {
        List<string> GetRegisteredPieceKeys();
        List<(int, int)> GetPossibleMoves(string description);
    }
}