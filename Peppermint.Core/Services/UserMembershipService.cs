using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    /// <summary>
    /// Manages user memberships to groups
    /// </summary>
    public class UserMembershipService : EntityService
    {
        private IDataAccessor<UserEntity> _userData;

        private IDataAccessor<UserGroupEntity> _userGroupData;
        private IDataAccessor<UserUserGroupEntity> _userUserGroupData;

        private IDataAccessor<RoleEntity> _roleData;
        private IDataAccessor<UserRoleEntity> _userRoleData;

        public UserMembershipService(
            IDataAccessor<UserEntity> userData,
            IDataAccessor<UserGroupEntity> userGroupData,
            IDataAccessor<UserUserGroupEntity> userUserGroupData,
            IDataAccessor<RoleEntity> roleData,
            IDataAccessor<UserRoleEntity> userRoleData)
        {
            _userData = userData;
            _userGroupData = userGroupData;
            _userUserGroupData = userUserGroupData;
            _roleData = roleData;
            _userRoleData = userRoleData;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersInGroup(int userGroupId)
        {
            var memberships = await _userUserGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserUserGroupEntity.UserGroupId), ConditionType.Equals, userGroupId)
            });

            var userIds = memberships.Select(membership => membership.UserId);

            var users = await _userData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserEntity.Id), ConditionType.In, userIds)
            });

            return users;
        }

        public async Task<IEnumerable<UserGroupEntity>> GetGroupsForUser(int userId)
        {
            var memberships = await _userUserGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserUserGroupEntity.UserId), ConditionType.Equals, userId)
            });

            var groupIds = memberships.Select(membership => membership.UserGroupId);

            var groups = await _userGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserEntity.Id), ConditionType.In, groupIds)
            });

            return groups;
        }

        public async Task<IEnumerable<RoleEntity>> GetRolesForUser(int userId)
        {
            var memberships = await _userRoleData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserRoleEntity.UserId), ConditionType.Equals, userId)
            });

            var roleIds = memberships.Select(membership => membership.RoleId);

            var roles = await _roleData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserEntity.Id), ConditionType.In, roleIds)
            });

            return roles;
        }
    }
}
