namespace Peppermint.Core.Data
{
    public interface IQueryBuilder
    {
        ISelectManyQuery<T> GetMany<T>();
        ISelectOneQuery<T> GetOne<T>();
        IUpdateQuery<T> Update<T>();
        IInsertQuery<T> Insert<T>();
    }
}