using Peppermint.App.Models;
using Peppermint.Core.Permissions;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class HeaderViewModel : ViewModel
    {
        AuthorizationService _authorization;
        public HeaderViewModel(AuthorizationService authorization, UserFactory userFactory) : base(userFactory)
        {
            _authorization = authorization;
        }

        public bool CanAccessAdmin { get; set; }
        public IEnumerable<NavLink> MainMenu { get; set; }
        public IEnumerable<NavLink> TopMenu { get; set; }

        public async Task<HeaderViewModel> Build()
        {
            var user = await GetUser();
            CanAccessAdmin = await _authorization.CanPerformAction(user?.Id, AdminPermissions.CanAccessAdmin);

            MainMenu = BuildMainMenu();
            TopMenu = BuildTopMenu();

            return this;
        }

        private IEnumerable<NavLink> BuildMainMenu()
        {
            return new List<NavLink>()
            {
                new NavLink()
                {
                    Label = "Home",
                    Location = "",
                    SubItems = new List<NavLink>()
                    {
                        new NavLink()
                        {
                            Label = "Blog",
                            Location = "blog"
                        }
                    }
                }
            };
        }

        private IEnumerable<NavLink> BuildTopMenu()
        {
            return new List<NavLink>()
            {
                new NavLink()
                {
                    Label = "Blog", Location = "blog",
                },
                new NavLink()
                {
                    Label = "Parent", Location = "#",
                    SubItems = new List<NavLink>()
                    {
                        new NavLink() { Label = "Child", Location = "#" }
                    }
                }
            };
        }
    }
}
