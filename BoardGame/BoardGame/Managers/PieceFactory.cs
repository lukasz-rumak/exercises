using System;
using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public class PieceFactory
    {
        private Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>();

        public void Register(string description, Type type)
        {
            _typeMapping.Add(description, type);
        }

        public IPiece GetPiece(string description)
        {
            return null;
            //return new _typeMapping[description];
        }
    }
}