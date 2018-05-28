using Peppermint.Core.Data;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class PostService : EntityService
    {
        private IDataAccessor<PostEntity> _forumPostData;
        public PostService(IDataAccessor<PostEntity> forumPostData)
        {
            _forumPostData = forumPostData;
        }

        public async Task<IEnumerable<PostEntity>> GetAllForumPosts()
        {
            var forumPosts = await _forumPostData.GetAll();
            return forumPosts;
        }

        public async Task<PostEntity> GetForumPost(int postId)
        {
            var forumPost = await _forumPostData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(PostEntity.Id), ConditionType.Equals, postId)
            });

            return forumPost;
        }
    }
}
