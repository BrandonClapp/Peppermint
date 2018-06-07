using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class AuthenticationService : EntityService
    {
        private EntityFactory _entityFactory;
        public AuthenticationService(IQueryBuilder query, EntityFactory entityFactory) : base(query)
        {
            _entityFactory = entityFactory;
        }

        // todo: real implementation
        public async Task<User> AuthenticateUser(string username, string password)
        {
            // Assume that checking the database takes 500ms
            await Task.Delay(500);

            if (username.ToLower() == "brandon" && password == "password")
            {
                var user = _entityFactory.Make<User>();
                user.UserName = "Brandon";
                user.Email = "brandon@email.com";
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
