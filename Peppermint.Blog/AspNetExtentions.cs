using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Reflection;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.Blog
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermintBlog(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            RegisterServices<EntityService>(assembly, services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(assembly, services, LifeStyle.Transient);

            return services;
        }
    }
}
