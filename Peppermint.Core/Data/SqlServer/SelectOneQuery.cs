using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public class SelectOneQuery<T> : SqlServerQuery, ISelectOneQuery<T>
    {
        public SelectOneQuery(string connString, EntityFactory entityFactory) : base(connString, entityFactory)
        {
            var schema = "core";
            var table = "users";
            _query = $"SELECT TOP 1 * FROM {schema}.{table}";
        }

        public new ISelectOneQuery<T> Where(string column, Is type, object value)
        {
            base.Where(column, type, value);
            return this;
        }

        public new ISelectOneQuery<T> And()
        {
            base.And();
            return this;
        }

        public new ISelectOneQuery<T> Or()
        {
            base.Or();
            return this;
        }

        public new ISelectOneQuery<T> StartGroup()
        {
            base.StartGroup();
            return this;
        }

        public new ISelectOneQuery<T> EndGroup()
        {
            base.EndGroup();
            return this;
        }

        public async Task<T> Execute()
        {
            return await GetFirstOrDefault<T>(_query, _parameters);
        }

    }
}
