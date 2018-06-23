using Peppermint.Core.Services;

namespace Peppermint.App.ViewModels.Admin
{
    public class AdminViewModel : ViewModel
    {
        public AdminViewModel(UserFactory userFactory) : base(userFactory)
        {
        }

        public AdminViewModel Build()
        {
            return this;
        }
    }
}
