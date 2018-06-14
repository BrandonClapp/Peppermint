using Peppermint.App.Models;
using Peppermint.Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogViewModel : ViewModel
    {
        private readonly PostService _postService;
        private readonly CategoryService _categoryService;
        public BlogViewModel(PostService postService, CategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        public Pagination Pagination { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public async Task<BlogViewModel> Build(int pageSize, int page, string categorySlug)
        {

            Posts = await BuildPosts(pageSize, page, categorySlug);
            this.Pagination = await BuildPagination(pageSize, page, categorySlug);

            return this;
        }

        private async Task<IEnumerable<Post>> BuildPosts(int pageSize, int page, string categorySlug)
        {
            var entities = await _postService.GetRecentPosts(pageSize, page, categorySlug);

            var posts = await Task.WhenAll(entities.Select(async e =>
            {
                var post = new Post()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Slug = e.Slug,
                    CategoryId = e.CategoryId,
                    Content = e.Content,
                    UserId = e.UserId,
                    Created = e.Created,
                    Updated = e.Updated
                };

                post.Thumbnail = await e.GetThumbnail();
                post.Banner = await e.GetBanner();
                post.Category = await e.GetCategory();
                post.User = await e.GetUser();

                return post;
            }));

            return posts;
        }

        private async Task<Pagination> BuildPagination(int pageSize, int page, string categorySlug)
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

            var totalPosts = await _categoryService.GetTotalPosts(categorySlug);
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
