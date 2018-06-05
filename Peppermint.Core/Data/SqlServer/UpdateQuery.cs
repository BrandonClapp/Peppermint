using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Peppermint.Core.Entities;

namespace Peppermint.Core.Data
{
    public class UpdateQuery<T> : SqlServerQuery, IUpdateQuery<T>
    {
        protected bool _setApplied;
        protected List<string> _sets;

        public UpdateQuery(string connString, EntityFactory entityFactory) : base(connString, entityFactory)
        {
            var schema = "core";
            var table = "Users";
            _query = $"UPDATE {schema}.{table}";
        }

        public IUpdateQuery<T> Set(string property, object value)
        {
            if (!_setApplied)
            {
                _query += " SET [SET]";
                _setApplied = true;
            }

            _sets.Add($"{property} = @{property}");
            _parameters.Add(property, value);

            return this;
        }

        public new IUpdateQuery<T> Where(string column, Is type, object value)
        {
            base.Where(column, type, value);
            return this;
        }

        public async Task Execute()
        {
            var query = Build();
            await UpdateItem(query, _parameters);
        }

        private string Build()
        {
            var setValues = string.Join(", ", _sets);
            _query = _query.Replace("[SET]", setValues);

            return _query;
        }
    }
}
