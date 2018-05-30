using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserGroups")]
    public class UserGroupEntity : DataEntity
    {
        private UserMembershipService _userMembershipService;

        public UserGroupEntity(UserMembershipService userMembershipService)
        {
            _userMembershipService = userMembershipService;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var users = await _userMembershipService.GetUsersInGroup(Id);
            return users;
        }
    }
}
