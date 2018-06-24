using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Admin
{
    public class AdminViewModel : ViewModel
    {
        AdminHeaderViewModel _header;

        public AdminViewModel(UserFactory userFactory, AdminHeaderViewModel header) : base(userFactory)
        {
            _header = header;
        }

        public AdminHeaderViewModel Header { get; set; }

        public async Task<AdminViewModel> Build()
        {
            Header = await _header.Build();
            return this;
        }
    }
}
