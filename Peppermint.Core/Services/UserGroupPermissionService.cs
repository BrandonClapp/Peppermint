using Peppermint.Core.Authorization;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserGroupPermissionService : EntityService
    {
        private IDataAccessor<UserGroupPermissionEnitity> _ugPermissionData;
        private IDataAccessor<PermissionEntity> _permissionData;

        public UserGroupPermissionService(
            IDataAccessor<UserGroupPermissionEnitity> ugPermissionData,
            IDataAccessor<PermissionEntity> permissionData)
        {
            _ugPermissionData = ugPermissionData;
            _permissionData = permissionData;
        }

        public async Task<bool> CanPerformAction(Permission permission, string groupEntityId = null)
        {
            var group = permission.PermissionGroup;

            var perm = await _permissionData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(PermissionEntity.Group), ConditionType.Equals, permission.PermissionGroup),
                new QueryCondition(nameof(PermissionEntity.Permission), ConditionType.Equals, permission.Value),
                new QueryCondition(nameof(PermissionEntity.Module), ConditionType.Equals, permission.Module)
            });

            if (perm == null)
                return false;

            var ugAll = await _ugPermissionData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(UserGroupPermissionEnitity.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(UserGroupPermissionEnitity.GroupEntityId), ConditionType.Equals, PermissionKey.All)
            });

            if (ugAll != null)
            {
                return ugAll.Permit;
            }

            var ugId = await _ugPermissionData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(UserGroupPermissionEnitity.PermissionId), ConditionType.Equals, perm.Id),
                new QueryCondition(nameof(UserGroupPermissionEnitity.GroupEntityId), ConditionType.Equals, groupEntityId)
            });

            if (ugId != null)
            {
                return ugId.Permit;
            }

            return false;
        }
        
    }
}
