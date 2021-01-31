using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPiece
    {
        int PieceId { get; set; }
        string PieceType { get; set; }
        bool IsAlive { get; set; }
        IPosition Position { get; }
        (int, int) CalculatePieceNewPosition();
        void ChangePiecePosition(int x, int y);
        void ChangeDirectionToRight();
        void ChangeDirectionToLeft();
    }
}