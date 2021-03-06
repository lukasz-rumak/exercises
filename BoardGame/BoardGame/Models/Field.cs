﻿using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.Models
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsTaken => TakenBy != null;
        public IPiece TakenBy { get; set; }
        
        public Field(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}