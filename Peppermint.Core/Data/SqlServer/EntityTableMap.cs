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

        public static void Register<T>(string table)
        {
            _map.Add(typeof(T), table);
        }

        public static string GetTable<T>() where T : DataEntity
        {
            _map.TryGetValue(typeof(T), out var type);
            return type;
        }
    }
}
