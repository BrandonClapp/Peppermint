using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Peppermint.App.ViewModels.Blog;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Blog
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly BlogListViewModel _blogViewModel;
        private readonly BlogPostViewModel _blogPostViewModel;

        public BlogController(BlogListViewModel blog, BlogPostViewModel blogPost)
        {
            _blogViewModel = blog;
            _blogPostViewModel = blogPost;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return await Category(null, null);
        }

        //[HttpGet("category/{categorySlug}")]
        //public async Task<IActionResult> Category(string categorySlug)
        //{
        //    var vm = await BuildListVm(categorySlug, null);
        //    return View("Index", vm);
        //}

        [HttpGet("category/{categorySlug}/{tagSlug?}")]
        public async Task<IActionResult> Category(string categorySlug, string tagSlug)
        {
            var vm = await BuildListVm(categorySlug, tagSlug);
            return View("Index", vm);
        }

        [HttpGet("tag/{tagSlug}")]
        public async Task<IActionResult> Tag(string tagSlug)
        {
            var vm = await BuildListVm(null, tagSlug);
            return View("Index", vm);
        }

        private async Task<BlogListViewModel> BuildListVm(string categorySlug, string tagSlug)
        {
            Request.Query.TryGetValue("page", out var page);

            if (string.IsNullOrEmpty(page))
                page = new StringValues("1");

            var pg = int.Parse(page);

            var pageSize = 5;
            var vm = await _blogViewModel.Build(pageSize, pg, categorySlug, tagSlug);

            return vm;
        }

        [HttpGet("{postSlug}")]
        public async Task<IActionResult> Post(string postSlug)
        {
            var vm = await _blogPostViewModel.Build(postSlug);
            return View(vm);
        }
    }
}
