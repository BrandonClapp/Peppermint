using Netify.Common.Data;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.SqlServer
{
    public class PostAbstraction : IPostDataAbstraction
    {
        private SqlServerDataAbstraction _data;
        private string _tableName = "Posts";

        public PostAbstraction(SqlServerDataAbstraction data)
        {
            _data = data;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _data.GetMany<Post>($"SELECT * FROM {_tableName}");
            return posts;
        }

        public async Task<Post> AddPost(Post post)
        {

            var addedId = await _data.AddItem(
                query: $@"
                    INSERT INTO {_tableName} (Title)
                        VALUES (@title)
                    SELECT CAST(SCOPE_IDENTITY() as int)
                ",
                parameters: new { post.Title }
            );

            var added = await _data.GetSingle<Post>(
                query: $"SELECT TOP 1 * FROM {_tableName} WHERE Id = @Id",
                parameters: new { Id = addedId }
            );

            return added;
        }
    }
}
