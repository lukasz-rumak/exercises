using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPiece
    {
        int PieceId { get; set; }
        string PieceType { get; set; }
        bool IsAlive { get; set; }
        IPosition Position { get; }
        IPosition MovePiece();
        void ChangeDirectionToRight();
        void ChangeDirectionToLeft();
    }
}