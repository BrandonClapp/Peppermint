using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Data.SqlServer
{
    public class DeleteOneQuery<T> : SqlServerQuery, IDeleteOneQuery<T>
    {
        public DeleteOneQuery(string connString, EntityFactory entityFactory, IDataLocationCache dataLocationCache)
            : base(connString, entityFactory, dataLocationCache)
        {
            _query = $"DELETE FROM [DATALOCATION]";
        }

        public new IDeleteOneQuery<T> Where(string column, Is comparison, object value)
        {
            base.Where(column, comparison, value);
            return this;
        }

        public async Task<int> Execute()
        {
            FillDataLocation<T>();
            return await DeleteItem(_query, _parameters);
        }
    }
}
