using Peppermint.Core.Entities.Forum;
using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
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
