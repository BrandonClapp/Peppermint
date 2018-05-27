using Netify.Common.Data;
using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.SqlServer.Abstractions
{
    public class PostAbstraction : IDataAccessor<PostEntity>
    {
        private SqlServerDataAbstraction _data;
        private readonly string _tableName = "Posts";

        public PostAbstraction(SqlServerDataAbstraction data)
        {
            _data = data;
        }

        public async Task<IEnumerable<PostEntity>> GetAll()
        {
            var posts = await _data.GetMany<PostEntity>($"SELECT * FROM {_tableName}");
            return posts;
        }

        public async Task<PostEntity> GetOne(int postId)
        {
            var post = await _data.GetFirstOrDefault<PostEntity>(
                query: $@"SELECT TOP 1 * FROM {_tableName} WHERE Id = @Id",
                parameters: new { Id = postId }
            );

            return post;
        }

        public async Task<PostEntity> Create(PostEntity postEntity)
        {
            var addedId = await _data.AddItem(
                query: $@"
                    INSERT INTO {_tableName} (UserId, Title, Content)
                        VALUES (@userId, @title, @content)
                ",
                parameters: new { postEntity.UserId, postEntity.Title, postEntity.Content }
            );

            var added = await GetOne(addedId);

            return added;
        }

        public async Task<PostEntity> Update(PostEntity postEntity)
        {
            await _data.UpdateItem(
                query: $@"
                    UPDATE {_tableName}
                        SET 
                            UserId = @userId,
                            Title = @title,
                            Content = @content
                        WHERE Id = @id
                ",
                parameters: new {
                    postEntity.Id,
                    postEntity.UserId,
                    postEntity.Title,
                    postEntity.Content
                });

            var updated = await GetOne(postEntity.Id);
            return updated;
        }

        public async Task<int> Delete(int postId)
        {
            var deletedId = await _data.DeleteItem(
                query: $@"
                    DELETE FROM {_tableName} WHERE Id = @id
                ",
                parameters: new { Id = postId }
            );

            return deletedId;
        }

        public async Task<PostEntity> GetOne(IEnumerable<QueryCondition> filters)
        {
            var (condition, parameters) = WhereBuilder.Build(filters);

            var post = await _data.GetFirstOrDefault<PostEntity>(
                query: $@"SELECT TOP 1 * FROM {_tableName} WHERE {condition}",
                parameters: parameters
            );

            return post;
        }

        public async Task<IEnumerable<PostEntity>> GetMany(IEnumerable<QueryCondition> filters)
        {
            var (condition, parameters) = WhereBuilder.Build(filters);

            var posts = await _data.GetMany<PostEntity>(
                query: $@"SELECT * FROM {_tableName} WHERE {condition}",
                parameters: parameters
            );

            return posts;
        }
    }
}
