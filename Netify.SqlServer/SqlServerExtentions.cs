using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Data;
using Netify.Common.Entities;
using System;
using System.Collections.Generic;

namespace Netify.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddNetifySqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString, fac.GetService<EntityFactory>()));
            services.AddTransient<IDataAccessor<PostEntity>, DataAccessor<PostEntity>>();
            services.AddTransient<IDataAccessor<UserEntity>, DataAccessor<UserEntity>>();

            ConfigureTableMappings();

            return services;
        }

        public static void ConfigureTableMappings()
        {
            var map = new Dictionary<Type, string>
            {
                [typeof(PostEntity)] = "Posts",
                [typeof(UserEntity)] = "Users",
            };

            EntityTableMap.Register(map);
        }
    }
}
