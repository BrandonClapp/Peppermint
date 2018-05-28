using Microsoft.AspNetCore.Mvc;
using Netify.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netify.Sample.Controllers
{
    [Route("api/[controller]")]
    public class ForumPostsController : Controller
    {
        private ForumPostService _forumPostService;
        public ForumPostsController(ForumPostService forumPostService)
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
                    post.ForumCategoryId,
                    post.PostId,
                    Post = await post.GetPost(),
                    Category = await post.GetForumCategory()
                };

                list.Add(forumPost);
            }

            return list;
        }

        [HttpGet("{postId}")]
        public async Task<dynamic> GetPost(int postId)
        {
            var forumPost = await _forumPostService.GetForumPost(postId);

            if (forumPost == null)
                return null;

            return new {
                forumPost.ForumCategoryId,
                forumPost.PostId,
                Post = await forumPost.GetPost(),
                Category = await forumPost.GetForumCategory()
            };
            
        }
    }
}
