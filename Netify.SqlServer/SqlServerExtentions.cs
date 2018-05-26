using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Data;

namespace Netify.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IPostDataAbstraction, PostAbstraction>();

            return services;
        }
    }
}
