using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Linq;
using System.Reflection;

namespace Peppermint.Core
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermint(this IServiceCollection services, string connectionString)
        {
            var assembly = Assembly.GetExecutingAssembly();

            RegisterServices<EntityService>(assembly, services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(assembly, services, LifeStyle.Transient);

            services.AddSingleton<EntityFactory>((fac) => new EntityFactory(fac));

            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString, fac.GetService<EntityFactory>()));

            services.AddTransient<IDataAccessor<UserEntity>, DataAccessor<UserEntity>>();

            return services;
        }

        public enum LifeStyle
        {
            Transient,
            Singleton
        }

        public static void RegisterEntities<TBase>(Assembly assembly, IServiceCollection services, LifeStyle lifeStyle) 
            where TBase : DataEntity
        {
            var baseType = typeof(TBase);
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

                var instance = (DataEntity)services.BuildServiceProvider().GetService(type);
                var dataLocation = instance.GetDataLocation();
                EntityTableMap.Register(type, dataLocation);
            }
        }

        public static void RegisterServices<TBase>(Assembly assembly, IServiceCollection services, LifeStyle lifeStyle)
            where TBase : EntityService
        {
            var baseType = typeof(TBase);
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
