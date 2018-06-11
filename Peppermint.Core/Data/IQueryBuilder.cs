namespace Peppermint.Core.Data
{
    public enum Is
    {
        EqualTo,
        Like,
        In
    }

    public enum Order
    {
        Ascending,
        Descending
    }

    public interface IQueryBuilder
    {
        ISelectManyQuery<T> GetMany<T>();
        ISelectOneQuery<T> GetOne<T>();
        IUpdateQuery<T> Update<T>();
        IInsertQuery<T> Insert<T>();
        IDeleteOneQuery<T> DeleteOne<T>();
        IDeleteManyQuery<T> DeleteMany<T>();
    }
}