using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Account
{
    public class LoginViewModel : ViewModel
    {
        HeaderViewModel _header;
        public LoginViewModel(UserFactory userFactory, HeaderViewModel header) : base(userFactory)
        {
            _header = header;
        }

        public HeaderViewModel Header { get; set; }

        public async Task<LoginViewModel> Build()
        {
            Header = await _header.Build();
            return this;
        }
    }
}
