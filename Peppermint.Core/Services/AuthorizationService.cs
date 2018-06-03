using Peppermint.Core.Authorization;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class AuthorizationService : EntityService
    {
        private UserMembershipService _userMembershipService;
        private IDataAccessor<Entities.Permission> _permissionData;
        private IDataAccessor<GroupPermission> _ugPermissionData;
        private IDataAccessor<RolePermission> _rolePermissionData;

        public AuthorizationService(
            UserMembershipService userMembershipService,
            IDataAccessor<Entities.Permission> permissionData,
            IDataAccessor<GroupPermission> ugPermissionData,
            IDataAccessor<RolePermission> rolePermissionData
            )
        {
            _permissionData = permissionData;
            _userMembershipService = userMembershipService;
            _ugPermissionData = ugPermissionData;
            _rolePermissionData = rolePermissionData;
        }

        public async Task<bool> CanPerformAction(int userId, Authorization.Permission permission, string groupEntityId = null)
        {
            var perm = await _permissionData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(Entities.Permission.Group), ConditionType.Equals, permission.PermissionGroup),
                new QueryCondition(nameof(Entities.Permission.Name), ConditionType.Equals, permission.Value),
                new QueryCondition(nameof(Entities.Permission.Module), ConditionType.Equals, permission.Module)
            });

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

        private async Task<bool> CanRolePerformAction(int userId, Entities.Permission perm, string groupEntityId = null)
        {
            var roles = await _userMembershipService.GetRolesForUser(userId);

            // todo: fix magic string for special role name.
            if (roles.Any(role => role.Name == "Administrator"))
            {
                return true;
            }

            var roleRightEntries = await _rolePermissionData.GetMany(new List<QueryCondition> {
                new QueryCondition(nameof(RolePermission.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(RolePermission.Permit), ConditionType.Equals, true)
            });

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

        private async Task<bool> CanGroupPerformAction(int userId, Entities.Permission perm, string groupEntityId = null)
        {
            var groups = await _userMembershipService.GetGroupsForUser(userId);

            // todo: fix magic string for special group name.
            if (groups.Any(group => group.Name == "Administrators"))
            {
                return true;
            }

            var groupRightEntries = await _ugPermissionData.GetMany(new List<QueryCondition> {
                new QueryCondition(nameof(GroupPermission.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(GroupPermission.Permit), ConditionType.Equals, true)
            });

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
