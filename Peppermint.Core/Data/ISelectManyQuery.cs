using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface ISelectManyQuery<T>
    {
        ISelectManyQuery<T> And();
        ISelectManyQuery<T> EndGroup();
        Task<IEnumerable<T>> Execute(object parameters = null);
        ISelectManyQuery<T> Or();
        ISelectManyQuery<T> StartGroup();
        ISelectManyQuery<T> Where(string column, Is type, object value);
    }
}