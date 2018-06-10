using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Reflection;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.Commerce
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermintCommerce(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            RegisterServices<EntityService>(assembly, services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(assembly, services, LifeStyle.Transient);

            return services;
        }
    }
}
