using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.Blog.Entities
{
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

        public override string GetDataLocation()
        {
            return $"{ModuleSettings.Schema}.Posts";
        }

        public async Task<UserEntity> GetUser()
        {
            return await _userService.GetUser(UserId);
        }
    }
}

