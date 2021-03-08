using System;
using System.Collections.Generic;
using BoardGame.Models;

namespace BoardGame.Managers
{
    public class ObjectFactory
    {
        private readonly Dictionary<Type, object> _dict = new Dictionary<Type, object>();

        public void Register<T>(T item)
        {
            _dict.Add(typeof(T), item);
        }

        public T Get<T>()
        {
            return (T) _dict[typeof(T)];
        }
    }
}