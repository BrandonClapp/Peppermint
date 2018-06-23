using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface ISelectManyQuery<T>
    {
        ISelectManyQuery<T> And();
        ISelectManyQuery<T> EndGroup();
        Task<IEnumerable<T>> Execute();
        ISelectManyQuery<T> Or();
        ISelectManyQuery<T> StartGroup();
        ISelectManyQuery<T> Where(string column, Is type, object value);
        ISelectManyQuery<T> Order(string column, Order order);
        ISelectManyQuery<T> Pagination(int pageSize, int page);
        ISelectManyQuery<T> InnerJoin<To>(string column, Is condition, object value);
    }
}