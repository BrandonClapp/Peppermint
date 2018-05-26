using Netify.Common.Data;
using Netify.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.SqlServer.Abstractions
{
    public class UserAbstraction : IUserDataAbstraction
    {
        private SqlServerDataAbstraction _userData;
        private string _tableName = "Users";

        public UserAbstraction(SqlServerDataAbstraction data)
        {
            _userData = data;
        }

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            var users = await _userData.GetMany<UserEntity>($@"
                SELECT * FROM {_tableName}
            ");

            return users;
        }

        public async Task<UserEntity> GetUser(int userId)
        {
            var user = await _userData.GetFirstOrDefault<UserEntity>($@"
                SELECT * FROM {_tableName} WHERE Id = @id",
                new { Id = userId }
            );

            return user;
        }

    }
}
