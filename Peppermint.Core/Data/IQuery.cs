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

    //public interface IQuery
    //{
    //    void And();
    //    void Or();
    //    void StartGroup();
    //    void EndGroup();
    //    void Where(string column, Is type, object value);

    //    Task<T> GetFirst<T>(string query, object parameters = null);
    //    Task<T> GetFirstOrDefault<T>(string query, object parameters = null);
    //    Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null);
    //    Task<T> GetSingle<T>(string query, object parameters = null);
    //    Task<T> GetSingleOrDefault<T>(string query, object parameters = null);

    //    Task UpdateItem(string query, object parameters);
    //    Task<int> AddItem(string query, object parameters);
    //    Task<int> DeleteItem(string query, object parameters);
    //}
}