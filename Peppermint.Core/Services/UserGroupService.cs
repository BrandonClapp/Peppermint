using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserGroupService : EntityService
    {
        private IQueryBuilder _query;
        public UserGroupService(IQueryBuilder query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _query.GetMany<Group>().Execute();
        }

        public async Task<Group> GetGroup(int userGroupId)
        {
            var group = await _query.GetOne<Group>()
                .Where(nameof(Group.Id), Is.EqualTo, userGroupId).Execute();

            return group;
        }

    }
}
