using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Peppermint.Core.Services
{
    public class UserFactory : EntityService
    {
        private User _user;
        private UserService _userService;
        private ClaimsIdentity _identity;

        public UserFactory(ClaimsIdentity identity, UserService userService, IQueryBuilder query) 
            : base(query)
        {
            _userService = userService;
            _identity = identity;
        }

        public async Task<User> GetCurrentUser()
        {
            if (_user != null)
                return _user;

            var claims = _identity.Claims.ToList();
            var username = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            var email = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(email))
                return null;

            _user = await _userService.GetUser(username, email);
            return _user;
        }

        public async Task<bool> IsAuthenticated()
        {
            var user = await GetCurrentUser();
            return user != null;
        }
    }
}
