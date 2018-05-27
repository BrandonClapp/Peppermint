using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Entities;
using Netify.Common.Services;

namespace Netify.Common
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddNetify(this IServiceCollection services)
        {
            services.AddTransient<PostService>();
            services.AddTransient<UserService>();

            // Entities
            services.AddTransient<UserEntity>();
            services.AddTransient<PostEntity>();
            // todo: Method to register all DataEntity classes in assembly in IoC container

            services.AddSingleton<EntityFactory>((fac) => new EntityFactory(fac));

            return services;
        }
    }
}
