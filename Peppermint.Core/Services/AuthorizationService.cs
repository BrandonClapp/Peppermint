using Peppermint.Core.Authorization;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class AuthorizationService : EntityService
    {
        private UserMembershipService _userMembershipService;

        public AuthorizationService(IQueryBuilder query, UserMembershipService userMembershipService)
            : base(query)
        {
            _userMembershipService = userMembershipService;
        }

        public async Task<bool> CanPerformAction(int? userId, PermissionType permission, string groupEntityId = null)
        {
            var perm = await _query.GetOne<Permission>()
                .Where(nameof(Permission.Group), Is.EqualTo, permission.PermissionGroup)
                .Where(nameof(Permission.Name), Is.EqualTo, permission.Value)
                .Where(nameof(Permission.Module), Is.EqualTo, permission.Module)
                .Execute();

            if (perm == null)
                return false;

            var roleWithPermission = await CanRolePerformAction(userId, perm, groupEntityId);
            if (roleWithPermission)
            {
                return true;
            }

            var inGroupWithPermission = await CanGroupPerformAction(userId, perm, groupEntityId);
            if (inGroupWithPermission)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> CanRolePerformAction(int? userId, Permission perm, string groupEntityId = null)
        {
            var roles = await _userMembershipService.GetRolesForUser(userId);

            // todo: fix magic string for special role name.
            if (roles.Any(role => role.Name == "Administrator"))
            {
                return true;
            }

            var roleRightEntries = await _query.GetMany<RolePermission>()
                .Where(nameof(RolePermission.PermissionId), Is.EqualTo, perm.Id)
                .Where(nameof(RolePermission.Permit), Is.EqualTo, true)
                .Execute();

            if (!roleRightEntries.Any())
            {
                return false;
            }

            var inRoleWithPermissions = roleRightEntries
                .Any(roleRight =>
                {
                    return roles.Any(role => role.Id == roleRight.RoleId) &&
                    (roleRight.GroupEntityId == "ALL" || roleRight.GroupEntityId == groupEntityId);
                });

            return inRoleWithPermissions;
        }

        private async Task<bool> CanGroupPerformAction(int? userId, Entities.Permission perm, string groupEntityId = null)
        {
            var groups = await _userMembershipService.GetGroupsForUser(userId);

            // todo: fix magic string for special group name.
            if (groups.Any(group => group.Name == "Administrators"))
            {
                return true;
            }

            var groupRightEntries = await _query.GetMany<GroupPermission>()
                .Where(nameof(GroupPermission.PermissionId), Is.EqualTo, perm.Id)
                .Where(nameof(GroupPermission.Permit), Is.EqualTo, true)
                .Execute();

            if (!groupRightEntries.Any())
            {
                return false;
            }

            var inGroupWithPermissions = groupRightEntries
                .Any(groupRight =>
                {
                    return groups.Any(group => group.Id == groupRight.UserGroupId) &&
                    (groupRight.GroupEntityId == "ALL" || groupRight.GroupEntityId == groupEntityId);
                });

            return inGroupWithPermissions;
        }

    }
}
