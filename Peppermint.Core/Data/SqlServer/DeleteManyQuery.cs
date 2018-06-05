using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Data.SqlServer
{
    public class DeleteManyQuery<T> : SqlServerQuery, IDeleteManyQuery<T>
    {
        public DeleteManyQuery(string connString, EntityFactory entityFactory, IDataLocationCache dataLocationCache) 
            : base(connString, entityFactory, dataLocationCache)
        {
            _query = $"DELETE FROM [DATALOCATION]";
        }

        public new IDeleteManyQuery<T> Where(string column, Is comparison, object value)
        {
            base.Where(column, comparison, value);
            return this;
        }

        public async Task Execute()
        {
            FillDataLocation<T>();
            await DeleteItems(_query, _parameters);
        }
    }
}
