using System;
using System.Collections.Generic;
using System.Linq;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class PieceFactory
    {
        private Dictionary<string, PieceFactoryTmp> _typeMapping = new Dictionary<string, PieceFactoryTmp>();

        public void Register(string description, PieceFactoryTmp type)
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