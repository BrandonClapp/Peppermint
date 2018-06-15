using Peppermint.App.Models;
using Peppermint.Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogPostViewModel : ViewModel
    {
        private readonly PostService _postService;
        public BlogPostViewModel(PostService postService)
        {
            _postService = postService;
        }

        public Post Post { get; set; }

        public async Task<BlogPostViewModel> Build(string postSlug)
        {
            var post = await BuildPost(postSlug);

            Post = post;

            return this;
        }

        private async Task<Post> BuildPost(string postSlug)
        {
            var entity = await _postService.GetPost(postSlug);

            var post = new Post()
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                CategoryId = entity.CategoryId,
                Content = entity.Content,
                UserId = entity.UserId,
                Created = entity.Created,
                Updated = entity.Updated
            };

            post.Html = await entity.GetHtml();
            post.Thumbnail = await entity.GetThumbnail();
            post.Banner = await entity.GetBanner();
            post.Category = await entity.GetCategory();
            post.User = await entity.GetUser();

            return post;
        }
    }
}
