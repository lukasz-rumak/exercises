using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;
using BoardGame.Managers;

namespace BoardGame.PieceFactory
{
    public class PieceFactory
    {
        private Dictionary<string, PieceAbstractFactory> _typeMapping = new Dictionary<string, PieceAbstractFactory>();

        public void Register(string description, PieceAbstractFactory type)
        {
            _typeMapping.Add(description, type);
        }

        public IPiece GetPiece(string description, int pieceId)
        {
            return _typeMapping.ContainsKey(description) ? _typeMapping[description].CreatePiece(pieceId) : new ErrorPiece();
        }

        public List<string> GetRegisteredKeys()
        {
            return _typeMapping.Keys.ToList();
        }
    }
}