﻿using BoardGame.Models;

namespace BoardGame.Interfaces
{
    public interface IPiece
    {
        int PieceId { get; set; }
        Piece PieceType { get; set; }
        bool IsAlive { get; set; }
        IPosition Position { get; }
        void ChangeDirectionToRight();
        void ChangeDirectionToLeft();
    }
}