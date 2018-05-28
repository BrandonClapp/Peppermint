using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
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

        public async Task<UserEntity> GetUser()
        {
            return await _userService.GetUser(UserId);
        }
    }
}
