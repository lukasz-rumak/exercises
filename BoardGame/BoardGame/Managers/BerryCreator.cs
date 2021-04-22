using System;
using System.Collections.Generic;
using BoardGame.Interfaces;
using BoardGame.Models;
using BoardGame.Models.Berries;

namespace BoardGame.Managers
{
    public class BerryCreator : IBerryCreator
    {
        private readonly Dictionary<BerryType, Func<string, IBerry>> _berrySelector;

        public BerryCreator()
        {
            _berrySelector = CreateBerrySelector();
        }

        public IBerry CreateBerryBasedOnType(BerryType berryType, string coordinates)
        {
            return _berrySelector[berryType](coordinates);
        }

        public BerryType MapToBerryType(string berryType)
        {
            return berryType switch
            {
                "B" => BerryType.BlueBerry,
                "S" => BerryType.StrawBerry,
                _ => BerryType.None
            };
        }

        private Dictionary<BerryType, Func<string, IBerry>> CreateBerrySelector()
        {
            return new Dictionary<BerryType, Func<string, IBerry>>
            {
                [BerryType.BlueBerry] = coordinates => CreateBlueBerry(coordinates),
                [BerryType.StrawBerry] = coordinates => CreateStrawBerry(coordinates),
                [BerryType.None] = coordinates => CreateErrorBerry(coordinates)
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
        
        private IBerry CreateErrorBerry(string coordinates)
        {
            return CreateBerry(coordinates, new ErrorBerry());
        }
    }
}