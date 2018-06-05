using Peppermint.Core.Data;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class PostService : EntityService
    {
        public PostService(IQueryBuilder query) : base (query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Post>> GetAllForumPosts()
        {
            var posts = await _query.GetMany<Post>().Execute();
            return posts;
        }

        public async Task<Post> GetForumPost(int postId)
        {
            var post = await _query.GetOne<Post>()
                .Where(nameof(Post.Id), Is.EqualTo, postId).Execute();

            return post;
        }
    }
}
