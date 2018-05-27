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

        [HttpGet("")]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return posts;
        }

        [HttpGet("{postId}")]
        public async Task<Post> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            return post;
        }

        [HttpGet("create")]
        public async Task<Post> CreatePost()
        {
            var postEntity = new PostEntity()
            {
                Content = "Entity content",
                Title = "Title here",
                UserId = 3
            };

            var post = await _postService.CreatePost(postEntity);
            return post;
        }

        [HttpGet("update/{postId}/{title}")]
        public async Task<Post> UpdatePost(int postId, string title)
        {
            var postEntity = new PostEntity()
            {
                Id = postId,
                Title = title,
                UserId = 2,
                Content = "Updated content"
            };

            var post = await _postService.UpdatePost(postEntity);
            return post;
        }

    }
}
