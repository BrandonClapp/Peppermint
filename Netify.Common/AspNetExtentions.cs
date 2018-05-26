using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient<PostService>();
            services.AddTransient<UserService>();

            return services;
        }
    }
}
