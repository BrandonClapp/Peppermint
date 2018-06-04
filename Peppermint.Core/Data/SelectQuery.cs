using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public enum SelectCount
    {
        One,
        Many
    }

    public class SelectQuery<T> : Query
    {
        protected string _query;
        protected bool _whereApplied;
        private SelectCount _count;

        public SelectQuery(string connString, SelectCount count, EntityFactory entityFactory)
            : base(connString, entityFactory)
        {
            // todo: check cache for table location
            // or look at attribute
            var schema = "core";
            var table = "Users";
            if (count == SelectCount.One)
            {
                _query = $"SELECT TOP 1 * FROM {schema}.{table}";
            }
            else if (count == SelectCount.Many)
            {
                _query = $"SELECT * FROM {schema}.{table}";
            }
        }

        public string Peek()
        {
            return _query;
        }
    }
}
