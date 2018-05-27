using Netify.Common.Data;
using Netify.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netify.Common.Services
{
    public class UserService
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
            var userEntity = await _userData.GetOne(userId);
            return userEntity;
        }
    }
}
