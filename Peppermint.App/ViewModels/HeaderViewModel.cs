using Peppermint.Core.Permissions;
using Peppermint.Core.Services;
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

        public async Task<HeaderViewModel> Build()
        {
            var user = await GetUser();
            CanAccessAdmin = await _authorization.CanPerformAction(user?.Id, AdminPermissions.CanAccessAdmin);
            return this;
        }

        public bool CanAccessAdmin { get; set; }
    }
}
