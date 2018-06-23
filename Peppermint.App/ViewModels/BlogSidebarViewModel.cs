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
        private CategoryService _categoryService;
        private TagService _tagService;

        public BlogSidebarViewModel(PostService postService, CategoryService categoryService,
            TagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public IEnumerable<Post> RecentPosts { get; set; }
        public IEnumerable<Post> PopularPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public bool EnableArchive { get; set; } = false;

        public async Task<BlogSidebarViewModel> Build()
        {
            var recentPosts = await BuildRecentPosts(3);
            var popularPosts = await BuildPopularPosts(3);
            var tags = await BuildTags();
            var categories = await BuildCategories();

            RecentPosts = recentPosts;
            PopularPosts = popularPosts;
            Tags = tags;
            Categories = categories;

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

        private async Task<IEnumerable<Category>> BuildCategories()
        {
            var entities = await _categoryService.GetCategories();
            var categoryIds = entities.Select(cat => cat.Id);
            var categories = new List<Category>();

            foreach (var category in entities)
            {
                Category cat = new Category()
                {
                    Id = category.Id,
                    Name = category.Name
                };

                cat.Slug = Slug.Create(cat.Name);

                var categoryTags = await _categoryService.GetTags(cat.Id);
                cat.Tags = categoryTags.Select(tag => {
                    var slug = Slug.Create(tag);
                    return new Tag(tag, slug);
                });

                categories.Add(cat);
            }

            return categories;
        }
    }
}
