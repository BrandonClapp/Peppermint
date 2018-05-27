using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Data;
using Netify.Common.Entities;
using Netify.Common.Services;
using Netify.SqlServer.Abstractions;

namespace Netify.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddNetifySqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString, fac.GetService<EntityFactory>()));
            services.AddTransient<IDataAccessor<PostEntity>, PostAbstraction>();
            services.AddTransient<IDataAccessor<UserEntity>, UserAbstraction>();

            return services;
        }
    }
}
