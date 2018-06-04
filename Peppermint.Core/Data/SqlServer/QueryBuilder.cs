using Peppermint.Core.Entities;
using System.Collections.Generic;

namespace Peppermint.Core.Data.SqlServer
{
    public static class QueryBuilder
    {
        private static string _connString;
        private static EntityFactory _entityFactory;

        public static void Initialize(string connString, EntityFactory entityFactory)
        {
            _connString = connString;
            _entityFactory = entityFactory;
        }

        public static SelectOneQuery<T> GetOne<T>()
        {
            return new SelectOneQuery<T>(_connString, SelectCount.One, _entityFactory);
        }

        public static SelectManyQuery<T> GetMany<T>()
        {
            return new SelectManyQuery<T>(_connString, SelectCount.Many, _entityFactory);
        }

    }
}
