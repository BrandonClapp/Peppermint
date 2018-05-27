using Netify.Common.Entities.Forum;
using Netify.Common.Services;
using System.Threading.Tasks;

namespace Netify.Common.Entities
{
    public class ForumPostEntity : PostEntity
    {
        private ForumCategoryService _forumCategoryService;

        public ForumPostEntity(UserService userService, ForumCategoryService forumCategoryService) : base(userService)
        {
            _forumCategoryService = forumCategoryService;
        }

        public int ForumCategoryId { get; set; }

        public async Task<ForumCategoryEntity> GetForumCategory()
        {
            return await _forumCategoryService.GetForumCategory(ForumCategoryId);
        }
    }
}
