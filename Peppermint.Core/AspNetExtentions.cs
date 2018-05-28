using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Linq;

namespace Peppermint.Core
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermint(this IServiceCollection services)
        {
            RegisterAll<EntityService>(services, LifeStyle.Transient);
            RegisterAll<DataEntity>(services, LifeStyle.Transient);

            services.AddSingleton<EntityFactory>((fac) => new EntityFactory(fac));

            return services;
        }

        public enum LifeStyle
        {
            Transient,
            Singleton
        }

        private static void RegisterAll<TBase>(IServiceCollection services, LifeStyle lifeStyle)
        {
            var baseType = typeof(TBase);
            var assembly = baseType.Assembly;

            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));

            foreach (var type in types)
            {
                if (lifeStyle == LifeStyle.Transient)
                {
                    services.AddTransient(type);
                }
                else if (lifeStyle == LifeStyle.Singleton)
                {
                    services.AddSingleton(type);
                }
            }
        }
    }
}
