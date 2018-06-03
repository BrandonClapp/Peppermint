using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Groups")]
    public partial class Group : DataEntity
    {
        private UserMembershipService _userMembershipService;

        public Group(UserMembershipService userMembershipService)
        {
            _userMembershipService = userMembershipService;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userMembershipService.GetUsersInGroup(Id);
            return users;
        }

    }
}
