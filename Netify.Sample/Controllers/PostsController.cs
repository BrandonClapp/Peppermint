using Microsoft.AspNetCore.Mvc;
using Netify.Common.Data;
using Netify.Common.Entities;
using Netify.Common.Models;
using Netify.Common.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.Sample.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{postId}")]
        public async Task<Post> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            return post;
        }

    }
}
