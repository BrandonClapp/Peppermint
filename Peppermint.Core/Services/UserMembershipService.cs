using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private IDataAccessor<UserUserGroupEntity> _userUserGroupEntityData;

        public UserMembershipService(
            IDataAccessor<UserEntity> userData,
            IDataAccessor<UserGroupEntity> userGroupData,
            IDataAccessor<UserUserGroupEntity> userUserGroupEntityData)
        {
            _userData = userData;
            _userGroupData = userGroupData;
            _userUserGroupEntityData = userUserGroupEntityData;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersInGroup(int userGroupId)
        {
            var memberships = await _userUserGroupEntityData.GetMany(new List<QueryCondition>
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
            var memberships = await _userUserGroupEntityData.GetMany(new List<QueryCondition>
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
    }
}
