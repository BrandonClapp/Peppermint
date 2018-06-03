using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserService : EntityService
    {
        private IDataAccessor<User> _userData;

        public UserService(IDataAccessor<User> userData)
        {
            _userData = userData;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var userEntities = await _userData.GetAll();
            return userEntities;
        }

        public async Task<User> GetUser(int userId)
        {
            var conditions = new List<QueryCondition>
            {
                new QueryCondition(nameof(User.Id), ConditionType.Equals, userId)
            };

            var userEntity = await _userData.GetOne(conditions);
            return userEntity;
        }

        public async Task<IEnumerable<User>> GetUsers(IEnumerable<int> userIds)
        {
            var users = await _userData.GetMany(new List<QueryCondition>
            {
                new QueryCondition(nameof(User.Id), ConditionType.In, userIds)
            });

            return users;
        }
    }
}
