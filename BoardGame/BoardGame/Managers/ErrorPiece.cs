using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class ErrorPiece : BerryCollector, IPiece
    {
        public int PieceId { get; set; }
        public string PieceType { get; set; }
        public bool IsAlive
        {
            get => false;
            set { }
        }
        public List<(int, int)> PossibleMoves { get; set; }
        public IPosition Position =>
            new Position
            {
                X = 9999,
                Y = 9999
            };
        
        public (int, int) CalculatePieceNewPosition()
        {
            throw new System.NotImplementedException();
        }

        public void ChangePiecePosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDirectionToRight()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDirectionToLeft()
        {
            throw new System.NotImplementedException();
        }
    }
}