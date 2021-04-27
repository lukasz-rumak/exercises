using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Models.Berries;

namespace BoardGame.Managers
{
    public class BerryCreator : IBerryCreator
    {
        private readonly Dictionary<string, Func<string, IBerry>> _berrySelector;

        public BerryCreator()
        {
            _berrySelector = CreateBerrySelector();
        }

        public IBerry CreateBerryBasedOnType(string berryType, string coordinates)
        {
            return _berrySelector.ContainsKey(berryType) ? _berrySelector[berryType](coordinates) : CreateBerry<ErrorBerry>(coordinates);
        }

        private Dictionary<string, Func<string, IBerry>> CreateBerrySelector()
        {
            return new Dictionary<string, Func<string, IBerry>>
            {
                ["B"] = CreateBerry<BlueBerry>,
                ["S"] = CreateBerry<StrawBerry>
            };
        }

        private IBerry CreateBerry<T>(string coordinates) where T : IBerry, new()
        {
            return new T
            {
                BerryPosition = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                IsEaten = false
            };
        }
    }
}