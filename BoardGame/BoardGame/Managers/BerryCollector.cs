using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public abstract class BerryCollector
    {
        private readonly List<IBerry> _collectedBerries = new List<IBerry>();
        
        public void CollectBerry(IBerry berry)
        {
            _collectedBerries.Add(berry);
        }

        public int CalculateScore()
        {
            return _collectedBerries.Count;
        }
    }
}