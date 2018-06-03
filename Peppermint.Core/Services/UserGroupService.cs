using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserGroupService : EntityService
    {
        private IDataAccessor<Group> _groupData;
        private IDataAccessor<UserGroup> _userGroupData;

        public UserGroupService(
            IDataAccessor<Group> groupData,
            IDataAccessor<UserGroup> userUserGroupData
            )
        {
            _groupData = groupData;
            _userGroupData = userUserGroupData;
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _groupData.GetAll();
        }

        public async Task<Group> GetGroup(int userGroupId)
        {
            var usergroup = await _groupData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(Group.Id), ConditionType.Equals, userGroupId)
            });

            return usergroup;
        }

    }
}
