using Netify.Common.Data;
using Netify.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.SqlServer.Abstractions
{
    public class UserAbstraction : IDataAccessor<UserEntity>
    {
        private SqlServerDataAbstraction _userData;
        private string _tableName = "Users";

        public UserAbstraction(SqlServerDataAbstraction data)
        {
            _userData = data;
        }

        public async Task<UserEntity> GetOne(int id)
        {
            var user = await _userData.GetFirstOrDefault<UserEntity>($@"
                SELECT * FROM {_tableName} WHERE Id = @id",
                new { Id = id }
            );

            return user;
        }

        public Task<UserEntity> GetOne(IEnumerable<QueryCondition> filters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            var users = await _userData.GetMany<UserEntity>($@"
                SELECT * FROM {_tableName}
            ");

            return users;
        }

        public Task<IEnumerable<UserEntity>> GetMany(IEnumerable<QueryCondition> filters)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserEntity> Create(UserEntity post)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserEntity> Update(UserEntity post)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Delete(int postId)
        {
            throw new System.NotImplementedException();
        }
    }
}
