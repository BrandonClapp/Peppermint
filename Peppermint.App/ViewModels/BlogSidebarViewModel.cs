using Peppermint.App.Extentions;
using Peppermint.App.Models;
using Peppermint.Blog.Services;
using Peppermint.Blog.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogSidebarViewModel : ViewModel
    {
        private PostService _postService;
        public BlogSidebarViewModel(PostService postService)
        {
            _postService = postService;
        }

        public IEnumerable<Post> RecentPosts { get; set; }
        public IEnumerable<Post> PopularPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public async Task<BlogSidebarViewModel> Build()
        {
            var recentPosts = await BuildRecentPosts(3);
            var popularPosts = await BuildPopularPosts(3);
            var tags = await BuildTags();

            RecentPosts = recentPosts;
            PopularPosts = popularPosts;
            Tags = tags;

            return this;
        }

        private async Task<IEnumerable<Post>> BuildRecentPosts(int count)
        {
            var entities = await _postService.GetRecentPosts(3, 1);
            return await entities.ToPosts();
        }

        private async Task<IEnumerable<Post>> BuildPopularPosts(int count)
        {
            var posts = await _postService.GetPopularPosts(count, null);
            return await posts.ToPosts();
        }

        private async Task<IEnumerable<Tag>> BuildTags()
        {
            var popular = await _postService.GetPopularTags(15);
            var tags = popular.Select(tag => new Tag(tag, Slug.Create(tag)));

            return tags;
        }
    }
}
