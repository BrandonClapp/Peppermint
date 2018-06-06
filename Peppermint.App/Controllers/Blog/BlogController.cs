using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Blog
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        public Task<dynamic> Index()
        {
            return Task.FromResult<dynamic>(new { Test = true });
        }
    }
}
