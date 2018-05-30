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
            _ugPermissionService = ugPermissionService;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var users = await _userMembershipService.GetUsersInGroup(Id);
            return users;
        }

        public async Task<bool> CanPerformAction<OnT, InCategoryT>(string action, int? entityId = null)
            where OnT : DataEntity
            where InCategoryT : DataEntity
        {
            var canPerformAction = await _ugPermissionService.CanPerformAction<OnT, InCategoryT>(Id, action, entityId);
            return canPerformAction;
        }
    }
}
