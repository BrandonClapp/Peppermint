using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface IDeleteManyQuery<T>
    {
        Task Execute();
        IDeleteManyQuery<T> Where(string column, Is comparison, object value);
    }
}