using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;

namespace Peppermint.SqlServer
{
    public static class EntityTableMap
    {
        private static IDictionary<Type, string> _map;

        public static void Register(IDictionary<Type, string> map)
        {
            _map = map;
        }

        public static string GetTable<T>() where T : DataEntity
        {
            _map.TryGetValue(typeof(T), out var type);
            return type;
        }
    }
}
