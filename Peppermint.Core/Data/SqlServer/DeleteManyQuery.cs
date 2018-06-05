using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Data.SqlServer
{
    public class DeleteManyQuery<T> : SqlServerQuery, IDeleteManyQuery<T>
    {
        public DeleteManyQuery(string connString, EntityFactory entityFactory) : base(connString, entityFactory)
        {
            var schema = "core";
            var table = "Users";
            _query = $"DELETE FROM {schema}.{table}";
        }

        public new IDeleteManyQuery<T> Where(string column, Is comparison, object value)
        {
            base.Where(column, comparison, value);
            return this;
        }

        public async Task Execute()
        {
            await DeleteItems(_query, _parameters);
        }
    }
}
