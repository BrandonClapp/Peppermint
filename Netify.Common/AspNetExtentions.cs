using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Netify.Common.Entities;
using Netify.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.Common
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddNetify(this IServiceCollection services)
        {
            ConfigureAutoMapper();

            services.AddTransient<PostService>();
            services.AddTransient<UserService>();

            // Entities
            services.AddTransient<UserEntity>();
            services.AddTransient<PostEntity>();
            // todo: Method to register all DataEntity classes in assembly in IoC container

            services.AddSingleton<EntityFactory>((fac) => new EntityFactory(fac));

            return services;
        }

        private static void ConfigureAutoMapper()
        {
            // todo: Method to register all DataEntity classes in assembly to AutoMapper
            // ^^ if needed.
            Mapper.Initialize(cfg => { });
        }
    }
}
