using Netify.Common.Data;
using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Services
{
    public class ForumPostService : EntityService
    {
        private IDataAccessor<ForumPostEntity> _forumPostData;
        public ForumPostService(IDataAccessor<ForumPostEntity> forumPostData)
        {
            _forumPostData = forumPostData;
        }

        public async Task<IEnumerable<ForumPostEntity>> GetAllForumPosts()
        {
            var forumPosts = await _forumPostData.GetAll();
            return forumPosts;
        }

        public async Task<ForumPostEntity> GetForumPost(int postId)
        {
            var forumPost = await _forumPostData.GetOne(new List<QueryCondition>
            {
                new QueryCondition("PostId", ConditionType.Equals, postId)
            });

            return forumPost;
        }
    }
}
