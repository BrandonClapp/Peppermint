using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using Peppermint.Core.Exceptions;
using Peppermint.Core.Services;
using System;
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
            services.AddSingleton<IDataLocationCache, DefaultDataLocationCache>();

            services.AddSingleton<IQueryBuilder, SqlServerQueryBuilder>(fac => {
                var entityFactory = fac.GetService<EntityFactory>();
                var dataLocationCache = fac.GetService<IDataLocationCache>();
                return new SqlServerQueryBuilder(connectionString, entityFactory, dataLocationCache);
            });

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
