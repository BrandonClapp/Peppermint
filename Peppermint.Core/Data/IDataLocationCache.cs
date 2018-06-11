namespace Peppermint.Core.Data
{
    public interface IDataLocationCache
    {
        string GetLocation<T>();
    }
}