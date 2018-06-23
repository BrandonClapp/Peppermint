using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Blog
{
    public class BlogViewModel : ViewModel
    {
        private BlogSidebarViewModel _sidebar;
        private HeaderViewModel _header;

        public BlogViewModel(UserFactory userFactory, HeaderViewModel header, BlogSidebarViewModel sidebar)
            : base(userFactory)
        {
            _sidebar = sidebar;
            _header = header;
        }

        public BlogSidebarViewModel Sidebar { get; set; }
        public HeaderViewModel Header { get; set; }

        public async Task Build()
        {
            Sidebar = await _sidebar.Build();
            Header = await _header.Build();
        }
    }
}
