using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Admin
{
    public class AdminViewModel : ViewModel
    {
        HeaderViewModel _header;

        public AdminViewModel(UserFactory userFactory, HeaderViewModel header) : base(userFactory)
        {
            _header = header;
        }

        public HeaderViewModel Header { get; set; }

        public async Task<AdminViewModel> Build()
        {
            Header = await _header.Build();
            return this;
        }
    }
}
