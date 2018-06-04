using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public enum Is
    {
        EqualTo,
        Like,
        In
    }

    public interface IQuery
    {
        Task<int> AddItem(string query, object parameters);
        void And();
        Task<int> DeleteItem(string query, object parameters);
        void EndGroup();
        Task<T> GetFirst<T>(string query, object parameters = null);
        Task<T> GetFirstOrDefault<T>(string query, object parameters = null);
        Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null);
        Task<T> GetSingle<T>(string query, object parameters = null);
        Task<T> GetSingleOrDefault<T>(string query, object parameters = null);
        void Or();
        void StartGroup();
        Task UpdateItem(string query, object parameters);
        void Where(string column, Is type, object value);
    }
}