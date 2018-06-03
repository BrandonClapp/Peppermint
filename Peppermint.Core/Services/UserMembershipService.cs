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
        private IDataAccessor<User> _userData;

        private IDataAccessor<Group> _userGroupData;
        private IDataAccessor<UserGroup> _userUserGroupData;

        private IDataAccessor<Role> _roleData;
        private IDataAccessor<UserRole> _userRoleData;

        public UserMembershipService(
            IDataAccessor<User> userData,
            IDataAccessor<Group> userGroupData,
            IDataAccessor<UserGroup> userUserGroupData,
            IDataAccessor<Role> roleData,
            IDataAccessor<UserRole> userRoleData)
        {
            _userData = userData;
            _userGroupData = userGroupData;
            _userUserGroupData = userUserGroupData;
            _roleData = roleData;
            _userRoleData = userRoleData;
        }

        public async Task<IEnumerable<User>> GetUsersInGroup(int userGroupId)
        {
            var memberships = await _userUserGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserGroup.UserGroupId), ConditionType.Equals, userGroupId)
            });

            var userIds = memberships.Select(membership => membership.UserId);

            var users = await _userData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(User.Id), ConditionType.In, userIds)
            });

            return users;
        }

        public async Task<IEnumerable<Group>> GetGroupsForUser(int userId)
        {
            var memberships = await _userUserGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserGroup.UserId), ConditionType.Equals, userId)
            });

            var groupIds = memberships.Select(membership => membership.UserGroupId);

            var groups = await _userGroupData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(User.Id), ConditionType.In, groupIds)
            });

            return groups;
        }

        public async Task<IEnumerable<Role>> GetRolesForUser(int userId)
        {
            var memberships = await _userRoleData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserRole.UserId), ConditionType.Equals, userId)
            });

            var roleIds = memberships.Select(membership => membership.RoleId);

            var roles = await _roleData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(User.Id), ConditionType.In, roleIds)
            });

            return roles;
        }
    }
}
