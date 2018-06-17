using Peppermint.App.Extentions;
using Peppermint.App.Models;
using Peppermint.Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogSidebarViewModel
    {
        private PostService _postService;
        public BlogSidebarViewModel(PostService postService)
        {
            _postService = postService;
        }

        public IEnumerable<Post> RecentPosts { get; set; }
        public IEnumerable<Post> PopularPosts { get; set; }

        public async Task<BlogSidebarViewModel> Build()
        {
            var recentPosts = await BuildRecentPosts(3);
            var popularPosts = await BuildPopularPosts(3);

            RecentPosts = recentPosts;
            PopularPosts = popularPosts;

            return this;
        }

        private async Task<IEnumerable<Post>> BuildRecentPosts(int count)
        {
            var entities = await _postService.GetRecentPosts(3, 1);
            return await entities.ToPosts();
        }

        private async Task<IEnumerable<Post>> BuildPopularPosts(int count)
        {
            return null;
        }
    }
}
