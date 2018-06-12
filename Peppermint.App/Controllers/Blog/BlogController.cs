using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Blog
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            Request.Query.TryGetValue("page", out var page);
            return View();
        }
    }
}
