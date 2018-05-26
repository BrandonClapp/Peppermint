using Netify.Common.Models;
using Netify.SqlServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.SqlServer
{
    public class TypeTableMapper
    {
        private static Dictionary<Type, string> _tableMap = new Dictionary<Type, string> {
            [typeof(Post)] = "Posts"
        };

        public static string GetTable<T>() where T : Model
        {
            var exists = _tableMap.TryGetValue(typeof(T), out var table);

            if (exists)
                return table;

            throw new UnrecognizedTableTypeException($"Unknown table for type {typeof(T).Name}");
        }
    }
}
