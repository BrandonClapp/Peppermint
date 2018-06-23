using Peppermint.App.Extentions;
using Peppermint.App.Models;
using Peppermint.Blog.Services;
using Peppermint.Blog.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogListViewModel : BlogViewModel
    {
        private readonly PostService _postService;
        private readonly CategoryService _categoryService;
        private readonly TagService _tagService;

        public BlogListViewModel(BlogSidebarViewModel sidebar, PostService postService,
            CategoryService categoryService, TagService tagService)
            : base (sidebar)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public Pagination Pagination { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }

        public async Task<BlogListViewModel> Build(int pageSize, int page, 
            string categorySlug = null, string tagSlug = null)
        {
            await base.Build();

            Posts = await BuildPosts(pageSize, page, categorySlug, tagSlug);
            Pagination = await BuildPagination(pageSize, page, categorySlug, tagSlug);
            Category = await BuildCategory(categorySlug);
            Tag = await BuildTag(tagSlug);

            return this;
        }

        private async Task<Category> BuildCategory(string categorySlug)
        {
            if (string.IsNullOrEmpty(categorySlug))
                return null;

            var category = await _categoryService.GetCategory(categorySlug);
            return await category.ToCategory();
        }

        private async Task<Tag> BuildTag(string tagSlug)
        {
            if (string.IsNullOrEmpty(tagSlug))
                return null;

            var tag = await Task.Run(() =>
            {
                var tagName = Slug.Reverse(tagSlug);
                var tagInfo = new Tag(tagName, tagSlug);
                return tagInfo;
            });

            return tag;
        }

        private async Task<IEnumerable<Post>> BuildPosts(int pageSize, int page,
            string categorySlug, string tagSlug)
        {
            if (!string.IsNullOrEmpty(categorySlug) && !string.IsNullOrEmpty(tagSlug))
            {
                var categoryTagPosts = await _postService.GetPostsByTagInCategory(categorySlug, tagSlug, pageSize, page);
                return await categoryTagPosts.ToPosts();
            }

            if (!string.IsNullOrEmpty(categorySlug))
            {
                var categoryPosts = await _postService.GetPostsByCategory(categorySlug, pageSize, page);
                return await categoryPosts.ToPosts();
            }

            if (!string.IsNullOrEmpty(tagSlug))
            {
                var tagPosts = await _postService.GetPostsByTag(tagSlug, pageSize, page);
                return await tagPosts.ToPosts();
            }

            var entities = await _postService.GetRecentPosts(pageSize, page);
            var posts = await entities.ToPosts();

            return posts;
        }

        // todo: fix to work with tags
        private async Task<Pagination> BuildPagination(int pageSize, int page,
            string categorySlug, string tagSlug)
        {
            var pagination = new Pagination()
            {
                CurrentPage = page
            };

            var minPage = page - 2;
            var prevPage = page - 1;
            pagination.CanGoPrevPage = true;
            if (minPage <= 1)
            {
                minPage = 1;
                prevPage = 1;
                pagination.CanGoPrevPage = false;
            }

            var totalPosts = 0;
            if (!string.IsNullOrEmpty(categorySlug))
            {
                totalPosts = await _categoryService.GetTotalPosts(categorySlug);
            }
            else if (!string.IsNullOrEmpty(tagSlug))
            {
                totalPosts = await _tagService.GetTotalPosts(tagSlug);
            }

            var pages = Math.Ceiling((double)totalPosts / pageSize);

            var maxPage = page + 2;
            var nextPage = page + 1;
            pagination.CanGoNextPage = true;

            // if the next page is off the radar, set cangonext page false
            if (page + 1 > pages)
            {
                pagination.CanGoNextPage = false;
                nextPage = page;
            }

            // if we're at the end of the page list, set max page to numer of pages
            if (maxPage >= pages)
            {
                maxPage = (int)pages;
            }

            pagination.MinPage = minPage;
            pagination.MaxPage = maxPage;
            pagination.PrevPage = prevPage;
            pagination.NextPage = nextPage;

            return pagination;
        }
    }
}
