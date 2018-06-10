using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peppermint.Core.Services;

namespace Peppermint.App.Controllers.Admin
{
    [Route("[controller]")]
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController(UserService userService)
        {

        }

        public IActionResult Index()
        {
            var identity = this.User.Identity;
            return View();
        }
    }
}
