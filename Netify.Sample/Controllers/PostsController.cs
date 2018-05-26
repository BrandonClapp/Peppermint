using Microsoft.AspNetCore.Mvc;
using Netify.Common.Data;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netify.Sample.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private IPostDataAbstraction _postData;

        public PostsController(IPostDataAbstraction postData)
        {
            _postData = postData;
        }

        [HttpGet("GetPosts")]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _postData.GetPosts();
            return posts;
        }
    }
}
