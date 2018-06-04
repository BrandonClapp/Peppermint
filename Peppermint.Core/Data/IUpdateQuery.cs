using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public interface IUpdateQuery<T>
    {
        Task Execute();
        IUpdateQuery<T> Set(string property, object value);
        IUpdateQuery<T> Where(string column, Is type, object value);
    }
}