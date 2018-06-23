using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserService : EntityService
    {
        public UserService(IQueryBuilder query) : base(query)
        {
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _query.GetMany<User>().Execute();
            return users;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _query.GetOne<User>()
                .Where(nameof(User.Id), Is.EqualTo, userId)
                .Execute();

            return user;
        }

        public async Task<User> GetUser(string username, string email)
        {
            var user = await _query.GetOne<User>()
                .Where(nameof(User.Email), Is.EqualTo, email)
                .Where(nameof(User.UserName), Is.EqualTo, username)
                .Execute();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(IEnumerable<int> userIds)
        {
            var users = await _query.GetMany<User>()
                .Where(nameof(User.Id), Is.In, userIds).Execute();

            return users;
        }

        //public async Task<User> GetCurrentUser()
        //{

        //}
    }
}
