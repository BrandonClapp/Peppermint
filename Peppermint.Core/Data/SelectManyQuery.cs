using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public class SelectManyQuery<T> : SelectQuery<T>
    {
        public SelectManyQuery(string connString, SelectCount count, EntityFactory entityFactory) : base(connString, count, entityFactory)
        {
        }

        public SelectManyQuery<T> Where(string column, Is type, object value)
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

        public SelectManyQuery<T> And()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " AND ";
            return this;
        }

        public SelectManyQuery<T> Or()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " OR ";
            return this;
        }

        public SelectManyQuery<T> StartGroup()
        {
            _query += " ( ";
            return this;
        }

        public SelectManyQuery<T> EndGroup()
        {
            _query += " ) ";
            return this;
        }

        public async Task<IEnumerable<T>> Execute(object parameters = null)
        {
            return await GetMany<T>(_query, parameters);
        }
    }
}
