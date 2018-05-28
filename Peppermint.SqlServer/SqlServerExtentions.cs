using Microsoft.Extensions.DependencyInjection;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Entities.Forum;
using System;
using System.Collections.Generic;

namespace Peppermint.SqlServer
{
    public static class SqlServerExtentions
    {
        public static IServiceCollection AddPeppermintSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(fac => new SqlServerDataAbstraction(connectionString, fac.GetService<EntityFactory>()));

            // Data accessors
            services.AddTransient<IDataAccessor<PostEntity>, DataAccessor<PostEntity>>();
            services.AddTransient<IDataAccessor<UserEntity>, DataAccessor<UserEntity>>();
            services.AddTransient<IDataAccessor<ForumPostEntity>, DataAccessor<ForumPostEntity>>();
            services.AddTransient<IDataAccessor<ForumCategoryEntity>, DataAccessor<ForumCategoryEntity>>();

            ConfigureTableMappings();

            return services;
        }

        public static void ConfigureTableMappings()
        {
            var map = new Dictionary<Type, string>
            {
                [typeof(PostEntity)] = "Posts",
                [typeof(UserEntity)] = "Users",
                [typeof(ForumPostEntity)] = "ForumPosts",
                [typeof(ForumCategoryEntity)] = "ForumCategories"
            };

            EntityTableMap.Register(map);
        }
    }
}
