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
            return _berrySelector[berryType](coordinates);
        }

        private Dictionary<string, Func<string, IBerry>> CreateBerrySelector()
        {
            return new Dictionary<string, Func<string, IBerry>>
            {
                ["B"] = coordinates => CreateBlueBerry(coordinates),
                ["S"] = coordinates => CreateStrawBerry(coordinates)
            };
        }

        private IBerry CreateBerry<T>(string coordinates, T berryType) where T : IBerry, new()
        {
            return new T
            {
                BerryPosition = (int.Parse(coordinates[0].ToString()), int.Parse(coordinates[1].ToString())),
                IsEaten = false
            };
        }
        
        private IBerry CreateBlueBerry(string coordinates)
        {
            return CreateBerry(coordinates, new BlueBerry());
        }

        private IBerry CreateStrawBerry(string coordinates)
        {
            return CreateBerry(coordinates, new StrawBerry());
        }
    }
}