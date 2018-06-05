using Peppermint.Core.Entities;

namespace Peppermint.Core.Data.SqlServer
{
    public class SqlServerQueryBuilder : IQueryBuilder
    {
        private readonly string _connString;
        private readonly EntityFactory _entityFactory;
        private readonly IDataLocationCache _dataLocationCache;

        public SqlServerQueryBuilder(string connString, EntityFactory entityFactory, IDataLocationCache dataLocationCache)
        {
            _connString = connString;
            _entityFactory = entityFactory;
            _dataLocationCache = dataLocationCache;
        }

        public ISelectOneQuery<T> GetOne<T>()
        {
            return new SelectOneQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }

        public ISelectManyQuery<T> GetMany<T>()
        {
            return new SelectManyQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }

        public IUpdateQuery<T> Update<T>()
        {
            return new UpdateQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }

        public IInsertQuery<T> Insert<T>()
        {
            return new InsertQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }

        public IDeleteOneQuery<T> DeleteOne<T>()
        {
            return new DeleteOneQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }

        public IDeleteManyQuery<T> DeleteMany<T>()
        {
            return new DeleteManyQuery<T>(_connString, _entityFactory, _dataLocationCache);
        }
    }
}
