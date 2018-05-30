using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;

namespace Peppermint.Core.Data.SqlServer
{
    public static class EntityTableMap
    {
        private static IDictionary<Type, string> _map;

        public static void Register(IDictionary<Type, string> map)
        {
            _map = map;
        }

        public static void Register(Type type, string table)
        {
            if (_map == null)
            {
                _map = new Dictionary<Type, string>();
            }

            _map.Add(type, table);
        }

        public static string GetTable<T>() where T : DataEntity
        {
            if (_map == null)
            {
                throw new NullReferenceException("Entity map not initialized. Register tables first.");
            }

            _map.TryGetValue(typeof(T), out var type);
            return type;
        }
    }
}
