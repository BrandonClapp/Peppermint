using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class ViewModel
    {
        private UserFactory _userFactory;

        public ViewModel(UserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        //public bool IsAuthenticated { get { return Task.Run(IsUserAuthenticated).Result; } }
        //public User User { get { return Task.Run(GetUser).Result; } }

        public async Task<bool> IsAuthenticated()
        {
            return await _userFactory.IsAuthenticated();
        }

        public async Task<User> GetUser()
        {
            return await _userFactory.GetCurrentUser();   
        }
    }
}
