using Peppermint.App.Models;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Admin
{
    public class AdminHeaderViewModel : ViewModel
    {
        public AdminHeaderViewModel(UserFactory userFactory) : base(userFactory)
        {
        }

        public async Task<AdminHeaderViewModel> Build()
        {
            MainMenu = new List<NavLink>()
            {
                new NavLink()
                {
                    Label = "Home", Location = ""
                },
                new NavLink()
                {
                    Label = "Dashboard", Location = "admin"
                },
                new NavLink()
                {
                    Label = "Navigation", Location = "admin/navigation"
                },
                new NavLink()
                {
                    Label = "Pages", Location = "admin/pages"
                },
                new NavLink()
                {
                    Label = "Media", Location = "admin/media",
                    SubItems = new List<NavLink>()
                    {
                        new NavLink() { Label = "Images", Location = "admin/media/images" },
                    }
                },
                new NavLink()
                {
                    Label = "Blog", Location = "admin/blog",
                    SubItems = new List<NavLink>()
                    {
                        new NavLink() { Label = "Posts", Location = "admin/blog/posts" },
                        new NavLink() { Label = "Categories", Location = "admin/blog/categories" },
                    }
                },
                new NavLink()
                {
                    Label = "Store", Location = "admin/store",
                    SubItems = new List<NavLink>()
                    {
                        new NavLink() { Label = "Catalog", Location = "admin/store/catalog" },
                        new NavLink() { Label = "Sources", Location = "admin/store/sources" },
                        new NavLink() { Label = "Categories", Location = "admin/store/categories" },
                        new NavLink() { Label = "Products", Location = "admin/store/products" },
                    }
                }
            };

            return this;
        }

        public bool CanAccessAdmin { get; set; } = true;
        public IEnumerable<NavLink> MainMenu { get; set; }
    }
}