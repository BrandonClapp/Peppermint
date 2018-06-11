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

        public ISelectManyQuery<T> Order(string column, Order order)
        {
            if (_query.TrimEnd().EndsWith("ORDER BY"))
            {
                _query += " ORDER BY ";
            }
            else
            {
                _query += ", ";
            }

            var direction = order == Data.Order.Descending ? "DESC" : "ASC";
            _query += $@" {column} {direction} ";

            return this;
        }

        public ISelectManyQuery<T> Pagination(int pageSize, int page)
        {
            var skipped = pageSize * (page - 1);
            _query += $@" OFFSET {skipped} ROWS FETCH NEXT {pageSize} ROWS ONLY ";
            return this;
        }
    }
}
