using Peppermint.Core.Services;

namespace Peppermint.App.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        public HomeViewModel(UserFactory userFactory) : base(userFactory)
        {
        }

        public HomeViewModel Build()
        {
            return this;
        }
    }
}
