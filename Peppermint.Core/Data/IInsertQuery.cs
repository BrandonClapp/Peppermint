using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface IInsertQuery<T>
    {
        Task<int> Execute();
        IInsertQuery<T> Value(string column, object value);
    }
}