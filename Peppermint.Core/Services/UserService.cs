using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserService : EntityService
    {
        private IDataAccessor<UserEntity> _userData;

        public UserService(IDataAccessor<UserEntity> userData)
        {
            _userData = userData;
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var userEntities = await _userData.GetAll();
            return userEntities;
        }

        public async Task<UserEntity> GetUser(int userId)
        {
            var conditions = new List<QueryCondition>
            {
                new QueryCondition(nameof(UserEntity.Id), ConditionType.Equals, userId)
            };

            var userEntity = await _userData.GetOne(conditions);
            return userEntity;
        }
    }
}
