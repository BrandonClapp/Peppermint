using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.Forum
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermintForum(this IServiceCollection services)
        {
            // Will this register all in blog or all in core?
            RegisterServices<EntityService>(services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(services, LifeStyle.Transient);

            services.AddTransient<IDataAccessor<ForumPostEntity>, DataAccessor<ForumPostEntity>>();
            services.AddTransient<IDataAccessor<ForumCategoryEntity>, DataAccessor<ForumCategoryEntity>>();

            return services;
        }
    }
}
