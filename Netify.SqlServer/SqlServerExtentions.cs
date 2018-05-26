using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Data;

namespace Netify.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection UseSqlServer(this IServiceCollection services)
        {
            services.AddTransient<IPostDataAbstraction, PostAbstraction>();

            return services;
        }
    }
}
