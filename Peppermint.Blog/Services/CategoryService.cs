using Peppermint.Blog.Entities;
using Peppermint.Core.Data;
using Peppermint.Core.Exceptions;
using Peppermint.Core.Extentions;
using Peppermint.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Blog.Services
{
    public class CategoryService : EntityService
    {
        private readonly TagService _tagService;
        public CategoryService(TagService tagService, IQueryBuilder query) : base(query)
        {
            _tagService = tagService;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _query.GetOne<Category>().Where(nameof(Category.Id), Is.EqualTo, id)
                .Execute();

            return category;
        }

        public async Task<Category> GetCategory(string slug)
        {
            var category = await _query.GetOne<Category>()
                .Where(nameof(Category.Slug), Is.EqualTo, slug)
                .Execute();

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _query.GetMany<Category>().Execute();
            return categories;
        }

        public async Task<IEnumerable<string>> GetTags(int id)
        {
            var category = await GetCategory(id);

            if (category == null)
                throw new ResourceNotFoundException($"Cannot get tags for category. " +
                    $"Category {id} not found.");

            var tags = await _tagService.GetCategoryPostTags(id);

            var unique = tags.GroupBy(tag => tag.Tag).Select(group => group.Key);
            return unique;
        }

        public async Task<IEnumerable<Post>> GetPosts(int id)
        {
            var posts = await _query.GetMany<Post>().Where(nameof(Post.CategoryId), Is.EqualTo, id)
                .Execute();

            return posts;
        }

        public async Task<int> GetTotalPosts(string categorySlug)
        {
            if (string.IsNullOrEmpty(categorySlug))
            {
                var posts = await _query.GetMany<Post>().Execute();
                return posts.Count();
            }

            var category = _query.GetOne<Category>().Where(nameof(Category.Slug), Is.EqualTo, categorySlug).Execute();
            if (category == null)
                throw new ArgumentException($"Category for category slug {categorySlug} not found.");

            return await GetTotalPosts(category.Id);
        }

        public async Task<int> GetTotalPosts(int categoryId)
        {
            // todo: optimize this by COUNT query in query builder
            var posts = await _query.GetMany<Post>()
                .Where(nameof(Category.Id), Is.EqualTo, categoryId).Execute();
            return posts.Count();
        }
    }
}
