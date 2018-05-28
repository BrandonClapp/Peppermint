using Microsoft.Extensions.DependencyInjection;
using Peppermint.Blog.Entities;
using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static Peppermint.Core.AspNetExtentions;

namespace Peppermint.Blog
{
    public static class AspNetExtentions
    {
        public static IServiceCollection AddPeppermintBlog(this IServiceCollection services)
        {
            // Will this register all in blog or all in core?
            RegisterServices<EntityService>(services, LifeStyle.Transient);
            RegisterEntities<DataEntity>(services, LifeStyle.Transient);

            services.AddTransient<IDataAccessor<PostEntity>, DataAccessor<PostEntity>>();

            return services;
        }
    }
}
