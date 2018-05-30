using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserGroupService : EntityService
    {
        private IDataAccessor<UserGroupEntity> _groupData;
        private IDataAccessor<UserUserGroupEntity> _userUserGroupData;

        public UserGroupService(
            IDataAccessor<UserGroupEntity> groupData,
            IDataAccessor<UserUserGroupEntity> userUserGroupData
            )
        {
            _groupData = groupData;
            _userUserGroupData = userUserGroupData;
        }

        public async Task<IEnumerable<UserGroupEntity>> GetAllGroups()
        {
            return await _groupData.GetAll();
        }

        public async Task<UserGroupEntity> GetGroup(int userGroupId)
        {
            var usergroup = await _groupData.GetOne(new List<QueryCondition>
            {
                new QueryCondition(nameof(UserGroupEntity.Id), ConditionType.Equals, userGroupId)
            });

            return usergroup;
        }

    }
}
