using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPiece
    {
        int PieceId { get; set; }
        string PieceType { get; set; }
        bool IsAlive { get; set; }
        List<(int, int)> PossibleMoves { get; set; }
        IPosition Position { get; }
        void CollectBerry(IBerry berry);
        int CalculateScore();
        (int, int) CalculatePieceNewPosition();
        void ChangePiecePosition(int x, int y);
        void ChangeDirectionToRight();
        void ChangeDirectionToLeft();
    }
}