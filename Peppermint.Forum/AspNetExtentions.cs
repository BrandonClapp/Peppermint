using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Reflection;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.Forum
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermintForum(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            RegisterServices<EntityService>(assembly, services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(assembly, services, LifeStyle.Transient);

            return services;
        }
    }
}
