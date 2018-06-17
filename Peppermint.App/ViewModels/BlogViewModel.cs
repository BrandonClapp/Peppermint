using Peppermint.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogViewModel : ViewModel
    {
        private BlogSidebarViewModel _sidebar;
        public BlogViewModel(BlogSidebarViewModel sidebar)
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
