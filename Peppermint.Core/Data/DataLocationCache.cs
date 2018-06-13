using Peppermint.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Peppermint.Core.Data
{
    public class DefaultDataLocationCache : IDataLocationCache
    {
        private static IDictionary<Type, string> _map = new Dictionary<Type, string>();
        private readonly object _mapLock = new object();

        public string GetLocation<T>()
        {
            lock(_mapLock)
            {
                if (_map.TryGetValue(typeof(T), out var location))
                {
                    return location;
                }

                DataLocation attr;

                try
                {
                    attr = typeof(T).GetCustomAttribute<DataLocation>();
                }
                catch (AmbiguousMatchException ex)
                {
                    throw new AmbiguousMatchException("More than one DataLocation attribute was found.", ex);
                }

                if (attr == null)
                {
                    throw new MissingExpectedAttributeException(nameof(DataLocation));
                }

                location = attr.GetLocation();
                _map.Add(typeof(T), location);

                return location;
            }
        }
    }
}
