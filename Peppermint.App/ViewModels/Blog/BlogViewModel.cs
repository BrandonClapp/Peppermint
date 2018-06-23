using Peppermint.Core.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Blog
{
    public class BlogViewModel : ViewModel
    {
        private BlogSidebarViewModel _sidebar;
        public BlogViewModel(UserFactory userFactory, BlogSidebarViewModel sidebar) : base(userFactory)
        {
            _sidebar = sidebar;
        }

        public BlogSidebarViewModel Sidebar { get; set; }

        public async Task Build()
        {
            Sidebar = await _sidebar.Build();
        }
    }
}
