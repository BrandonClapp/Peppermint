using Peppermint.Core.Services;

namespace Peppermint.App.ViewModels.Account
{
    public class LoginViewModel : ViewModel
    {
        public LoginViewModel(UserFactory userFactory) : base(userFactory)
        {
        }

        public LoginViewModel Build()
        {
            return this;
        }
    }
}
