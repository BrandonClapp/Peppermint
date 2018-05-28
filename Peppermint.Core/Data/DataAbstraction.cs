using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface IDataAbstraction
    {
        Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null) where T : DataEntity;

        Task<T> GetSingle<T>(string query, object parameters = null) where T : DataEntity;
        Task<T> GetSingleOrDefault<T>(string query, object parameters = null) where T : DataEntity;

        Task<T> GetFirst<T>(string query, object parameters = null) where T : DataEntity;
        Task<T> GetFirstOrDefault<T>(string query, object parameters = null) where T : DataEntity;

        Task<int> AddItem(string query, object parameters);
        Task UpdateItem(string query, object parameters);
    }
}
