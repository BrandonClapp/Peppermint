using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peppermint.App.ViewModels.Admin;
using Peppermint.Core.Permissions;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize(Policy = nameof(AdminPermissions.CanAccessAdmin))]
    public class AdminController : Controller
    {
        AdminViewModel _adminView;
        public AdminController(AdminViewModel adminView)
        {
            _adminView = adminView;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _adminView.Build();
            return View(vm);
        }
    }
}
