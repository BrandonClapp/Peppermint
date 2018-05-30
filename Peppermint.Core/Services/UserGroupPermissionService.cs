using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserGroupPermissionService : EntityService
    {
        private IDataAccessor<UserGroupPermissionEnitity> _ugPermissionData;
        private IDataAccessor<PermissionTypeEntity> _permissionTypeData;

        public UserGroupPermissionService(
            IDataAccessor<UserGroupPermissionEnitity> ugPermissionData,
            IDataAccessor<PermissionTypeEntity> permissionTypeData)
        {
            _ugPermissionData = ugPermissionData;
        }

        public async Task<bool> CanPerformAction<OnT>(int usergroupId, string category, string action, int? entityId = null) 
            where OnT : DataEntity
        {
            var permission = await _permissionTypeData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(PermissionTypeEntity.PermissionCategory), ConditionType.Equals, category),
                new QueryCondition(nameof(PermissionTypeEntity.PermissionName), ConditionType.Equals, action),
                new QueryCondition(nameof(PermissionTypeEntity.EntityType), ConditionType.Equals, typeof(OnT).Name),
            });

            if (permission == null)
                return false; // should this be false?

            var ugPermission = await _ugPermissionData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserGroupPermissionEnitity.PermissionTypeId), ConditionType.Equals, permission.Id),
                new QueryCondition(nameof(UserGroupPermissionEnitity.UserGroupId), ConditionType.Equals, usergroupId),
                new QueryCondition(nameof(UserGroupPermissionEnitity.EntityQualifier), ConditionType.Equals, entityId)
            });

            var canPerformAction = ugPermission.CanPerformAction;
            return canPerformAction;
        }

        // todo: public async Task<UserGroupPermission> AddUserGroupPermission<ForT>(int usergroupId, string category, string action, int? entityId = null) 
        //              where ForT : DataEntity{}
    }
}
