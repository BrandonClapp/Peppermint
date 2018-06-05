using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class GroupService : EntityService
    {
        public GroupService(IQueryBuilder query) : base(query)
        {
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
