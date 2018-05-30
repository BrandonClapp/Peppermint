using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Users")]
    public class UserEntity : DataEntity
    {
        private UserService _userService;
        private UserMembershipService _userMembershipService;

        public UserEntity(UserService userService, UserMembershipService userMembershipService)
        {
            _userService = userService;
            _userMembershipService = userMembershipService;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public async Task<IEnumerable<UserGroupEntity>> GetGroups()
        {
            return await _userMembershipService.GetGroupsForUser(Id);
        }
    }
}
