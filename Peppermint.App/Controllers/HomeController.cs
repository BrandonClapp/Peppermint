using Microsoft.AspNetCore.Mvc;
using Peppermint.App.ViewModels.Home;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var vm = await _homeView.Build();
            return View(vm);
        }
    }
}
