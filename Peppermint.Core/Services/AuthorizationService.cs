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
        private IDataAccessor<PermissionEntity> _permissionData;
        private IDataAccessor<UserGroupPermissionEntity> _ugPermissionData;
        private IDataAccessor<RolePermissionEntity> _rolePermissionData;

        public AuthorizationService(
            UserMembershipService userMembershipService,
            IDataAccessor<PermissionEntity> permissionData,
            IDataAccessor<UserGroupPermissionEntity> ugPermissionData,
            IDataAccessor<RolePermissionEntity> rolePermissionData
            )
        {
            _permissionData = permissionData;
            _userMembershipService = userMembershipService;
            _ugPermissionData = ugPermissionData;
            _rolePermissionData = rolePermissionData;
        }

        public async Task<bool> CanPerformAction(int userId, Permission permission, string groupEntityId = null)
        {
            var perm = await _permissionData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(PermissionEntity.Group), ConditionType.Equals, permission.PermissionGroup),
                new QueryCondition(nameof(PermissionEntity.Permission), ConditionType.Equals, permission.Value),
                new QueryCondition(nameof(PermissionEntity.Module), ConditionType.Equals, permission.Module)
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

        private async Task<bool> CanRolePerformAction(int userId, PermissionEntity perm, string groupEntityId = null)
        {
            var roles = await _userMembershipService.GetRolesForUser(userId);

            // todo: fix magic string for special role name.
            if (roles.Any(role => role.Name == "Administrator"))
            {
                return true;
            }

            var roleRightEntries = await _rolePermissionData.GetMany(new List<QueryCondition> {
                new QueryCondition(nameof(RolePermissionEntity.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(RolePermissionEntity.Permit), ConditionType.Equals, true)
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

        private async Task<bool> CanGroupPerformAction(int userId, PermissionEntity perm, string groupEntityId = null)
        {
            var groups = await _userMembershipService.GetGroupsForUser(userId);

            // todo: fix magic string for special group name.
            if (groups.Any(group => group.Name == "Administrators"))
            {
                return true;
            }

            var groupRightEntries = await _ugPermissionData.GetMany(new List<QueryCondition> {
                new QueryCondition(nameof(UserGroupPermissionEntity.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(UserGroupPermissionEntity.Permit), ConditionType.Equals, true)
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
