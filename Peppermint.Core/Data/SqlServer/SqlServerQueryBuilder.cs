﻿using Peppermint.Core.Entities;

namespace Peppermint.Core.Data.SqlServer
{
    public class SqlServerQueryBuilder : IQueryBuilder
    {
        private readonly string _connString;
        private readonly EntityFactory _entityFactory;

        public SqlServerQueryBuilder(string connString, EntityFactory entityFactory)
        {
            _connString = connString;
            _entityFactory = entityFactory;
        }

        public ISelectOneQuery<T> GetOne<T>()
        {
            return new SelectOneQuery<T>(_connString, _entityFactory);
        }

        public ISelectManyQuery<T> GetMany<T>()
        {
            return new SelectManyQuery<T>(_connString, _entityFactory);
        }

        public IUpdateQuery<T> Update<T>()
        {
            return new UpdateQuery<T>(_connString, _entityFactory);
        }

        public IInsertQuery<T> Insert<T>()
        {
            return new InsertQuery<T>(_connString, _entityFactory);
        }

        public IDeleteOneQuery<T> DeleteOne<T>()
        {
            return new DeleteOneQuery<T>(_connString, _entityFactory);
        }

        public IDeleteManyQuery<T> DeleteMany<T>()
        {
            return new DeleteManyQuery<T>(_connString, _entityFactory);
        }
    }
}