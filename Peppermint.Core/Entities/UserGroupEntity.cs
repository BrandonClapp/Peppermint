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
        private UserGroupPermissionService _ugPermissionService;

        public UserGroupEntity(
            UserMembershipService userMembershipService,
            UserGroupPermissionService ugPermissionService)
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

        public async Task<bool> CanPerformAction<OnT>(string category, string action, int? entityId = null)
            where OnT : DataEntity
        {
            var canPerformAction = await _ugPermissionService.CanPerformAction<OnT>(Id, category, action, entityId);
            return canPerformAction;
        }
    }
}
