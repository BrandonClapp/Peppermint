using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface IDeleteOneQuery<T>
    {
        Task<int> Execute();
        IDeleteOneQuery<T> Where(string column, Is comparison, object value);
    }
}