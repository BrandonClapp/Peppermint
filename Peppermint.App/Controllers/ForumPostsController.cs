using Microsoft.AspNetCore.Mvc;
using Peppermint.Core.Services;
using Peppermint.Forum.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers
{
    [Route("api/[controller]")]
    public class ForumPostsController : Controller
    {
        private PostService _forumPostService;
        public ForumPostsController(PostService forumPostService)
        {
            _forumPostService = forumPostService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<dynamic>> GetPosts()
        {
            var forumPosts = await _forumPostService.GetAllForumPosts();
            var list = new List<dynamic>();

            foreach (var post in forumPosts)
            {
                var forumPost = new
                {
                    post.Id,
                    post.CategoryId,
                    post.Title,
                    post.Content,
                    Category = await post.GetCategory(),
                    User = await post.GetUser()
                };

                list.Add(forumPost);
            }

            return list;
        }

        [HttpGet("{postId}")]
        public async Task<dynamic> GetPost(int postId)
        {
            var post = await _forumPostService.GetForumPost(postId);

            if (post == null)
                return null;

            return new
            {
                post.Id,
                post.CategoryId,
                post.Title,
                post.Content,
                Category = await post.GetCategory(),
                User = await post.GetUser()
            };

        }
    }
}
