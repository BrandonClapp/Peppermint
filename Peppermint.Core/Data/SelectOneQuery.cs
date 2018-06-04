using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Peppermint.Core.Entities;

namespace Peppermint.Core.Data
{
    public class SelectOneQuery<T> : SelectQuery<T>
    {
        public SelectOneQuery(string connString, SelectCount count, EntityFactory entityFactory) : base(connString, count, entityFactory)
        {
        }

        public SelectOneQuery<T> Where(string column, Is type, object value)
        {
            if (!_whereApplied)
            {
                _query += " WHERE ";
                _whereApplied = true;
            }

            if (type == Is.EqualTo)
            {
                _query += $"{column} = @{column}";
            }
            else if (type == Is.Like)
            {
                _query += $"{column} LIKE %@{column}%";
            }
            else if (type == Is.In)
            {
                _query += $"{column} IN @{column}";
            }

            return this;
        }

        public SelectOneQuery<T> And()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " AND ";
            return this;
        }

        public SelectOneQuery<T> Or()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " OR ";
            return this;
        }

        public SelectOneQuery<T> StartGroup()
        {
            _query += " ( ";
            return this;
        }

        public SelectOneQuery<T> EndGroup()
        {
            _query += " ) ";
            return this;
        }

        public async Task<T> Execute(object parameters = null)
        {
            return await GetFirstOrDefault<T>(_query, parameters);
        }
    }
}
