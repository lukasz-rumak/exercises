using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Managers
{
    public abstract class BerryCollector
    {
        protected readonly List<IBerry> CollectedBerries = new List<IBerry>();
        
        public void CollectBerry(IBerry berry)
        {
            CollectedBerries.Add(berry);
        }

        public virtual int CalculateScore()
        {
            return CollectedBerries.Count;
        }
    }
}