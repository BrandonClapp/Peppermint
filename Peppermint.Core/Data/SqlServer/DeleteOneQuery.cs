using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Data.SqlServer
{
    public class DeleteOneQuery<T> : SqlServerQuery, IDeleteOneQuery<T>
    {
        public DeleteOneQuery(string connString, EntityFactory entityFactory) : base(connString, entityFactory)
        {
            var schema = "core";
            var table = "Users";
            _query = $"DELETE FROM {schema}.{table}";
        }

        public new IDeleteOneQuery<T> Where(string column, Is comparison, object value)
        {
            base.Where(column, comparison, value);
            return this;
        }

        public async Task<int> Execute()
        {
            return await DeleteItem(_query, _parameters);
        }
    }
}
