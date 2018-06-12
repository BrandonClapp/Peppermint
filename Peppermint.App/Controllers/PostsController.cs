using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peppermint.Blog.Entities;
using Peppermint.Blog.Services;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private PostService _postService;
        private EntityFactory _entityFactory;

        public PostsController(PostService postService, EntityFactory entityFactory)
        {
            _postService = postService;
            _entityFactory = entityFactory;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return posts;
        }

        [HttpGet("recent")]
        public async Task<IEnumerable<Post>> GetPosts(int pageSize, int page)
        {
            var posts = await _postService.GetRecentPosts(pageSize, page);
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
            dynamic entity = new ExpandoObject();
            entity.Content = "Entity content";
            entity.Title = "Title here";
            entity.UserId = 3;

            var postEntity = _entityFactory.Make<Post>(entity);

            var post = await _postService.CreatePost(postEntity);
            return post;
        }

        [HttpGet("update/{postId}/{title}")]
        public async Task<Post> UpdatePost(int postId, string title)
        {
            var postEntity = _entityFactory.Make<Post>();
            postEntity.Id = postId;
            postEntity.Title = title;
            postEntity.UserId = 2;
            postEntity.Content = "Updated content from ctrl";

            var post = await _postService.UpdatePost(postEntity);
            return post;
        }

        [HttpGet("user/{userName}")]
        public async Task<IEnumerable<Post>> GetByUserName(string userName)
        {
            var posts = await _postService.GetPosts(userName);
            return posts;
        }

    }
}
