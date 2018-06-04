using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface ISelectOneQuery<T>
    {
        ISelectOneQuery<T> And();
        ISelectOneQuery<T> EndGroup();
        Task<T> Execute(object parameters = null);
        ISelectOneQuery<T> Or();
        ISelectOneQuery<T> StartGroup();
        ISelectOneQuery<T> Where(string column, Is type, object value);
    }
}