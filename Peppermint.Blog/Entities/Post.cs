using HeyRed.MarkdownSharp;
using Peppermint.Blog.Services;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Blog.Entities
{
    [DataLocation("blog.Posts")]
    public class Post : DataEntity
    {
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;
        private readonly PostService _postService;

        public Post(UserService userService, CategoryService categoryService, PostService postService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _postService = postService;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public async Task<string> GetHtml()
        {
            return await Task.Run(() =>
            {
                var markdown = new Markdown().Transform(Content);
                return markdown;
            });
        }

        // Image uploads are something that can be baked into core.
        // Change this to attachments?
        public async Task<string> GetThumbnail()
        {
            return await Task.FromResult("/assets/img/preview/blog/blog-post-1.jpg");
        }

        public async Task<string> GetBanner()
        {
            return await Task.FromResult("/assets/img/preview/blog/blog-post-1.jpg");
        }

        public async Task<User> GetUser()
        {
            return await _userService.GetUser(UserId);
        }

        public async Task<Category> GetCategory()
        {
            return await _categoryService.GetCategory(CategoryId);
        }

        public async Task<IEnumerable<PostTag>> GetTags()
        {
            return await _postService.GetPostTags(Id);
        }
    }
}

