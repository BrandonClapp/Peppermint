using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public class SelectManyQuery<T> : SqlServerQuery, ISelectManyQuery<T>
    {
        public SelectManyQuery(string connString, EntityFactory entityFactory, IDataLocationCache dataLocationCache)
            : base(connString, entityFactory, dataLocationCache)
        {
            _query = $"SELECT * FROM [DATALOCATION]";
        }

        public new ISelectManyQuery<T> Where(string column, Is type, object value)
        {
            base.Where(column, type, value);
            return this;
        }

        public new ISelectManyQuery<T> And()
        {
            base.And();
            return this;
        }

        public new ISelectManyQuery<T> Or()
        {
            base.Or();
            return this;
        }

        public new ISelectManyQuery<T> StartGroup()
        {
            base.StartGroup();
            return this;
        }

        public new ISelectManyQuery<T> EndGroup()
        {
            base.EndGroup();
            return this;
        }

        public async Task<IEnumerable<T>> Execute()
        {
            FillDataLocation<T>();
            return await GetMany<T>(_query, _parameters);
        }
    }
}
