using Microsoft.AspNetCore.Mvc;
using Peppermint.App.ViewModels;

namespace Peppermint.App.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        HomeViewModel _homeView;
        public HomeController(HomeViewModel homeView)
        {
            _homeView = homeView;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var vm = _homeView.Build();
            return View(vm);
        }
    }
}
