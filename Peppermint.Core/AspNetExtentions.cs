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
            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString, fac.GetService<EntityFactory>()));

            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransient<IDataAccessor<User>, DataAccessor<User>>();
            services.AddTransient<IDataAccessor<Permission>, DataAccessor<Permission>>();

            services.AddTransient<IDataAccessor<Group>, DataAccessor<Group>>();
            services.AddTransient<IDataAccessor<UserGroup>, DataAccessor<UserGroup>>();
            services.AddTransient<IDataAccessor<GroupPermission>, DataAccessor<GroupPermission>>();

            services.AddTransient<IDataAccessor<Role>, DataAccessor<Role>>();
            services.AddTransient<IDataAccessor<RolePermission>, DataAccessor<RolePermission>>();
            services.AddTransient<IDataAccessor<UserRole>, DataAccessor<UserRole>>();

            RegisterServices<EntityService>(assembly, services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(assembly, services, LifeStyle.Transient);

            services.AddSingleton<EntityFactory>((fac) => new EntityFactory(fac));

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

                var dataLocation = GetDataLocation(type);
                EntityTableMap.Register(type, dataLocation);
            }
        }

        private static string GetDataLocation(Type type)
        {
            DataLocation attr;

            try
            {
                attr = type.GetCustomAttribute<DataLocation>();
            }
            catch (AmbiguousMatchException ex)
            {
                throw new AmbiguousMatchException("More than one DataLocation attribute was found.", ex);
            }

            if (attr == null)
            {
                throw new MissingExpectedAttributeException(nameof(DataLocation));
            }

            var location = attr.GetLocation();
            return location;
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
