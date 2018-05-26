using Netify.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IUserDataAbstraction
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity> GetUser(int userId);
    }
}
