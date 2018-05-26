using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Data;
using Netify.Common.Services;
using Netify.SqlServer.Abstractions;

namespace Netify.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddNetifySqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString));
            services.AddTransient<IPostDataAbstraction, PostAbstraction>();
            services.AddTransient<IUserDataAbstraction, UserAbstraction>();

            return services;
        }
    }
}
