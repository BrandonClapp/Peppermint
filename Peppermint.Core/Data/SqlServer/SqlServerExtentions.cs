using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;

namespace Peppermint.Core.Data.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddPeppermintSqlServer(this IServiceCollection services, string connectionString)
        {
            

            // Data accessors


            

            ConfigureTableMappings();

            return services;
        }

        public static void ConfigureTableMappings()
        {
            // todo: abstract prop/method to get table/location.
            var map = new Dictionary<Type, string>
            {
                [typeof(UserEntity)] = "Users",
            };

            EntityTableMap.Register(map);
        }
    }
}
