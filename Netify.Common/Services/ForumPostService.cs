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
        private IDataAccessor<PostEntity> _postData;
        private IDataAccessor<ForumPostEntity> _forumPostData;
        public ForumPostService(
            IDataAccessor<PostEntity> postData,
            IDataAccessor<ForumPostEntity> forumPostData
            )
        {
            // Posts
            // ForumPosts
            // ForumCategories
            _postData = postData;
            _forumPostData = forumPostData;
        }

        public async Task<ForumPostEntity> GetForumPost(int postId)
        {
            var post = await _postData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(PostEntity.Id), ConditionType.Equals, postId)
            });

            var forumPost = await _forumPostData.GetOne(new List<QueryCondition>
            {
                new QueryCondition("PostId", ConditionType.Equals, post.Id)
            });

            //forumPost.
            // forumPost will be the inherited class... 
            // .. but GetOne queries pivot table
            // think about how to handle joins
        }
    }
}
