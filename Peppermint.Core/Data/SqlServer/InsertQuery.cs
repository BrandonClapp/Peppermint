using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Core.Data.SqlServer
{
    public class InsertQuery<T> : SqlServerQuery, IInsertQuery<T>
    {
        private List<string> _columns = new List<string>();

        public InsertQuery(string connString, EntityFactory entityFactory) : base(connString, entityFactory)
        {
            var schema = "core";
            var table = "Users";
            _query = $"INSERT INTO {schema}.{table} ([COLUMNS]) VALUES ([VALUES])";
        }

        public IInsertQuery<T> Value(string column, object value)
        {
            _columns.Add(column);
            _parameters.Add(column, value);
            return this;
        }

        public IInsertQuery<T> Values(T item, params string[] identities)
        {
            var propType = typeof(T);
            foreach (var prop in propType.GetProperties())
            {
                if (identities.Contains(prop.Name))
                    continue;

                var value = prop.GetValue(item);
                Value(prop.Name, value);
            }

            return this;
        }

        public async Task<int> Execute()
        {
            var query = Build();
            return await AddItem(query, _parameters);
        }

        private string Build()
        {
            var columns = string.Join(", ", _columns);
            var values = string.Join(", ", _columns.Select(c => $"@{c}"));
            var query = _query.Replace("[COLUMNS]", columns).Replace("[VALUES]", values);
            return query;
        }

        
    }
}
