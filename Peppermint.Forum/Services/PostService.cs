using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class PostService : EntityService
    {
        private IDataAccessor<Post> _forumPostData;
        public PostService(IDataAccessor<Post> forumPostData)
        {
            _forumPostData = forumPostData;
        }

        public async Task<IEnumerable<Post>> GetAllForumPosts()
        {
            var forumPosts = await _forumPostData.GetAll();
            return forumPosts;
        }

        public async Task<Post> GetForumPost(int postId)
        {
            QueryBuilder.DefineQuery().Get<Post>(SelectCount.One)
                .Where(nameof(Post.Id), Is.EqualTo, postId).BuildAndExecute();

            //_forumPostData.GetOne().Where(nameof(Post.Id), Is.EqualTo, postId).Execute();
            var forumPost = await _forumPostData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(Post.Id), Is.EqualTo, postId)
            });

            return forumPost;
        }
    }
}
