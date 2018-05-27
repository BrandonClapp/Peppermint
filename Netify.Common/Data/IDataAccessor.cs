using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IDataAccessor<T> where T : DataEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetOne(IEnumerable<QueryCondition> conditions);

        Task<IEnumerable<T>> GetMany(IEnumerable<QueryCondition> conditions);

        Task<T> Create(IEnumerable<InsertQueryParameter> queryParameters);
        Task<T> Update(IEnumerable<UpdateQueryParameter> queryParameters);
        Task<int> Delete(IEnumerable<QueryCondition> conditions);
    }
}
