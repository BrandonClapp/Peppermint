using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IDataAccessor<T> where T : DataEntity
    {
        Task<T> GetOne(int id);
        Task<T> GetOne(IEnumerable<QueryCondition> filters);

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetMany(IEnumerable<QueryCondition> filters);

        Task<T> Create(T post);
        Task<T> Update(T post);
        Task<int> Delete(int postId);
    }
}
