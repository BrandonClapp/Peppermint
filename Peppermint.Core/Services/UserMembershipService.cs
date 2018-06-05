using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
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
        public UserMembershipService(IQueryBuilder query) : base(query)
        {
            _query = query;
        }

        public async Task<IEnumerable<User>> GetUsersInGroup(int userGroupId)
        {
            var memberships = await _query.GetMany<UserGroup>()
                .Where(nameof(UserGroup.UserGroupId), Is.EqualTo, userGroupId).Execute();

            var userIds = memberships.Select(membership => membership.UserId);

            var users = await _query.GetMany<User>()
                .Where(nameof(User.Id), Is.In, userIds).Execute();

            return users;
        }

        public async Task<IEnumerable<Group>> GetGroupsForUser(int userId)
        {
            var memberships = await _query.GetMany<UserGroup>()
                .Where(nameof(UserGroup.UserId), Is.EqualTo, userId).Execute();

            var groupIds = memberships.Select(membership => membership.UserGroupId);

            var groups = await _query.GetMany<Group>()
                .Where(nameof(User.Id), Is.In, groupIds).Execute();

            return groups;
        }

        public async Task<IEnumerable<Role>> GetRolesForUser(int userId)
        {
            var memberships = await _query.GetMany<UserRole>()
                .Where(nameof(UserRole.UserId), Is.EqualTo, userId).Execute();

            var roleIds = memberships.Select(membership => membership.RoleId);

            var roles = await _query.GetMany<Role>()
                .Where(nameof(User.Id), Is.In, roleIds).Execute();

            return roles;
        }
    }
}
