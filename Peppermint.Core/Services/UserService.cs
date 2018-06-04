using Peppermint.Core.Data;
using Peppermint.Core.Data.SqlServer;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserService : EntityService
    {
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await QueryBuilder.GetMany<User>().Execute();
            return users;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await QueryBuilder.GetOne<User>()
                .Where(nameof(User.Id), Is.EqualTo, userId)
                .Execute();

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(IEnumerable<int> userIds)
        {
            var users = await QueryBuilder.GetMany<User>()
                .Where(nameof(User.Id), Is.In, userIds).Execute();

            return users;
        }
    }
}
