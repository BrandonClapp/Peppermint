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
        private readonly PostService _postService;
        public BlogController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            Request.Query.TryGetValue("page", out var page);

            if (string.IsNullOrEmpty(page))
                page = new StringValues("1");

            var pg = int.Parse(page);

            var entities = await _postService.GetRecentPosts(5, pg);

            var posts = await Task.WhenAll(entities.Select(async e =>
            {
                var post = new Post()
                {
                    Id = e.Id,
                    Title = e.Title,
                    CategoryId = e.CategoryId,
                    Content = e.Content,
                    UserId = e.UserId,
                    Created = e.Created,
                    Updated = e.Updated
                };

                post.Thumbnail = await e.GetThumbnail();
                post.Banner = await e.GetBanner();
                post.Category = await e.GetCategory();
                post.User = await e.GetUser();

                return post;
            }));

            

            var vm = new BlogViewModel()
            {
                Posts = posts
            };

            return View(vm);
        }
    }
}
