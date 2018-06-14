using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Peppermint.App.Models;
using Peppermint.App.ViewModels;
using Peppermint.Blog.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Blog
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly BlogViewModel _blogViewModel;
        private readonly BlogPostViewModel _blogPostViewModel;

        public BlogController(BlogViewModel blog, BlogPostViewModel blogPost)
        {
            _blogViewModel = blog;
            _blogPostViewModel = blogPost;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return await Category(null);
        }

        [HttpGet("category/{categorySlug}")]
        public async Task<IActionResult> Category(string categorySlug)
        {
            Request.Query.TryGetValue("page", out var page);

            if (string.IsNullOrEmpty(page))
                page = new StringValues("1");

            var pg = int.Parse(page);

            var pageSize = 5;
            var vm = await _blogViewModel.Build(pageSize, pg, categorySlug);

            return View("Index", vm);
        }

        [HttpGet("{postSlug}")]
        public async Task<IActionResult> Post(string postSlug)
        {
            var vm = await _blogPostViewModel.Build(postSlug);
            return View(vm);
        }
    }
}
