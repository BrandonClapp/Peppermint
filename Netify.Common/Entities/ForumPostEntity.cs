using Peppermint.Common.Entities.Forum;
using Peppermint.Common.Services;
using System.Threading.Tasks;

namespace Peppermint.Common.Entities
{
    public class ForumPostEntity : DataEntity
    {
        private ForumCategoryService _forumCategoryService;
        private PostService _postService;

        public ForumPostEntity(
            ForumCategoryService forumCategoryService,
            PostService postService)
        {
            _forumCategoryService = forumCategoryService;
            _postService = postService;
        }

        public int PostId { get; set; }
        public int ForumCategoryId { get; set; }

        public async Task<ForumCategoryEntity> GetForumCategory()
        {
            return await _forumCategoryService.GetForumCategory(ForumCategoryId);
        }

        public async Task<PostEntity> GetPost()
        {
            return await _postService.GetPost(PostId);
        }
    }
}
