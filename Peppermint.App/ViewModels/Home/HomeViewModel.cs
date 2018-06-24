using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Home
{
    public class HomeViewModel : ViewModel
    {
        HeaderViewModel _header;
        public HomeViewModel(UserFactory userFactory, HeaderViewModel header) : base(userFactory)
        {
            _header = header;
        }

        public HeaderViewModel Header;

        public async Task<HomeViewModel> Build()
        {
            Header = await _header.Build();
            return this;
        }
    }
}
