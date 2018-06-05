using Peppermint.Core.Data;

namespace Peppermint.Core.Services
{
    /// <summary>
    /// Base class for all services
    /// </summary>
    public class EntityService
    {
        protected IQueryBuilder _query;
        public EntityService(IQueryBuilder query)
        {
            _query = query;
        }
    }
}
