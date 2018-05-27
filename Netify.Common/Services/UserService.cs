using Netify.Common.Data;
using Netify.Common.Entities;
using Netify.Common.Exceptions;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Services
{
    public class UserService
    {
        private IUserDataAbstraction _userData;

        public UserService(IUserDataAbstraction userData)
        {
            _userData = userData;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var userEntities = await _userData.GetUsers();

            var users = userEntities.Select(ue => Construct(ue));
            return users;
        }

        public async Task<User> GetUser(int userId)
        {
            var userEntity = await _userData.GetUser(userId);

            var user = Construct(userEntity);
            return user;
        }

        // todo: automapper if they stay the same
        private User Construct(UserEntity userEntity)
        {
            if (userEntity == null)
                return null;

            return new User()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                UserName = userEntity.UserName
            };
        }
    }
}
