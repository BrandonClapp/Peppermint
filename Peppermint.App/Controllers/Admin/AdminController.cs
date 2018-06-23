using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peppermint.App.ViewModels.Admin;
using Peppermint.Core.Services;

namespace Peppermint.App.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize]
    public class AdminController : Controller
    {
        AdminViewModel _adminView;
        public AdminController(AdminViewModel adminView)
        {
            _adminView = adminView;
        }

        public IActionResult Index()
        {
            var vm = _adminView.Build();
            return View(vm);
        }
    }
}
