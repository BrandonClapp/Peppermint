using Netify.Common.Data;
using Netify.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.SqlServer.Abstractions
{
    public class PostAbstraction : IPostDataAbstraction
    {
        private SqlServerDataAbstraction _data;
        private readonly string _tableName = "Posts";

        public PostAbstraction(SqlServerDataAbstraction data)
        {
            _data = data;
        }

        public async Task<IEnumerable<PostEntity>> GetPosts()
        {
            var posts = await _data.GetMany<PostEntity>($"SELECT * FROM {_tableName}");
            return posts;
        }

        public async Task<PostEntity> AddPost(PostEntity post)
        {
            var addedId = await _data.AddItem(
                query: $@"
                    INSERT INTO {_tableName} (UserId, Title, Content)
                        VALUES (@userId, @title, @content)
                ",
                parameters: new { post.UserId, post.Title, post.Content }
            );

            var added = await GetPost(addedId);

            return added;
        }

        public async Task<PostEntity> GetPost(int postId)
        {
            var post = await _data.GetFirstOrDefault<PostEntity>(
                query: $@"SELECT TOP 1 * FROM {_tableName} WHERE Id = @Id",
                parameters: new { Id = postId }
            );

            return post;
        }
    }
}
