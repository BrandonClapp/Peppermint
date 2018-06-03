using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.Blog.Entities
{
    [DataLocation("blog.Posts")]
    public class PostEntity : DataEntity
    {
        private UserService _userService;

        public PostEntity(UserService userService)
        {
            _userService = userService;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public async Task<User> GetUser()
        {
            return await _userService.GetUser(UserId);
        }
    }
}

