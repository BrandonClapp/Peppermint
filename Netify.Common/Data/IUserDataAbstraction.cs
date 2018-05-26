using Netify.Common.Entities;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IUserDataAbstraction
    {
        Task<UserEntity> GetUser(int userId);
    }
}
