using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using Peppermint.Forum.Services;
using System.Threading.Tasks;

namespace Peppermint.Forum.Entities
{
    [DataLocation("forum.Posts")]
    public partial class PostEntity : DataEntity
    {
        private CategoryService _forumCategoryService;
        private UserService _userService;

        public PostEntity(CategoryService forumCategoryService, UserService userService)
        {
            _forumCategoryService = forumCategoryService;
            _userService = userService;
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public async Task<CategoryEntity> GetCategory()
        {
            return await _forumCategoryService.GetForumCategory(CategoryId);
        }

        public async Task<User> GetUser()
        {
            return await _userService.GetUser(UserId);
        }
    }
}
