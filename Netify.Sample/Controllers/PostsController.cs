using Microsoft.AspNetCore.Mvc;
using Netify.Common.Entities;
using Netify.Common.Services;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Netify.Sample.Controllers
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
        public async Task<IEnumerable<PostEntity>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return posts;
        }

        [HttpGet("{postId}")]
        public async Task<PostEntity> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            return post;
        }

        [HttpGet("create")]
        public async Task<PostEntity> CreatePost()
        {
            dynamic entity = new ExpandoObject();
            entity.Content = "Entity content";
            entity.Title = "Title here";
            entity.UserId = 3;

            var postEntity = _entityFactory.Make<PostEntity>(entity);

            var post = await _postService.CreatePost(postEntity);
            return post;
        }

        [HttpGet("update/{postId}/{title}")]
        public async Task<PostEntity> UpdatePost(int postId, string title)
        {
            var postEntity = _entityFactory.Make<PostEntity>();
            postEntity.Id = postId;
            postEntity.Title = title;
            postEntity.UserId = 2;
            postEntity.Content = "Updated content from ctrl";

            var post = await _postService.UpdatePost(postEntity);
            return post;
        }

        [HttpGet("user/{userName}")]
        public async Task<IEnumerable<PostEntity>> GetByUserName(string userName)
        {
            var posts = await _postService.GetPosts(userName);
            return posts;
        }

    }
}
