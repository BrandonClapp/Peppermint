using Peppermint.Blog.Entities;
using Peppermint.Blog.Utilities;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Blog.Services
{
    public class PostService : EntityService
    {
        public PostService(IQueryBuilder query) : base(query)
        {
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _query.GetMany<Post>().Execute();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetRecentPosts(int pageSize = 15, int page = 1)
        {
            var posts = await _query.GetMany<Post>()
                .Order(nameof(Post.Id), Order.Descending)
                .Pagination(pageSize, page)
                .Execute();

            return posts;
        }

        public async Task<IEnumerable<Post>> GetPostsByTag(string tagSlug, int pageSize = 15, int page = 1)
        {
            if (string.IsNullOrEmpty(tagSlug))
                return null;

            var tagName = Slug.Reverse(tagSlug);
            var tags = await _query.GetMany<PostTag>()
                .Where(nameof(PostTag.Tag), Is.EqualTo, tagName).Execute();

            if (tags != null && tags.Any())
            {
                var posts = await _query.GetMany<Post>()
                .Where(nameof(Post.Id), Is.In, tags.Select(t => t.PostId))
                .Order(nameof(Post.Id), Order.Descending)
                .Pagination(pageSize, page)
                .Execute();

                return posts;
            }

            return null;
        }

        public async Task<IEnumerable<Post>> GetPostsByTagInCategory(string categorySlug, string tagSlug, int pageSize = 15, int page = 1)
        {
            // todo: standardize return null vs throwing exceptions
            if (string.IsNullOrEmpty(categorySlug) || string.IsNullOrEmpty(tagSlug))
                return null;

            var category = await _query.GetOne<Category>()
                    .Where(nameof(Category.Slug), Is.EqualTo, categorySlug).Execute();

            var tagName = Slug.Reverse(tagSlug);

            if (category == null)
                return null;

            var postsInCategory = await GetPostsByCategory(categorySlug, 1000, 1);
            var postIds = postsInCategory.Select(post => post.Id);
            
            return await _query.GetMany<Post>()
                    .InnerJoin<PostTag>(nameof(Post.Id), nameof(PostTag.PostId))
                    .Where(nameof(PostTag.PostId), Is.In, postIds)
                    .Where(nameof(PostTag.Tag), Is.EqualTo, tagName)
                    .Order(nameof(Post.Id), Order.Descending)
                    .Pagination(pageSize, page)
                    .Execute();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(string categorySlug, int pageSize = 15, int page = 1)
        {
            if (string.IsNullOrEmpty(categorySlug))
                return null;

            var category = await _query.GetOne<Category>()
                    .Where(nameof(Category.Slug), Is.EqualTo, categorySlug).Execute();

            if (category == null)
                return null;

            return await _query.GetMany<Post>()
                    .Where(nameof(Post.CategoryId), Is.EqualTo, category.Id)
                    .Order(nameof(Post.Id), Order.Descending)
                    .Pagination(pageSize, page)
                    .Execute();
        }

        public async Task<IEnumerable<Post>> GetPopularPosts(int count, string categorySlug)
        {
            var query = _query.GetMany<Post>(count);

            if (!string.IsNullOrEmpty(categorySlug))
            {
                var category = await _query.GetOne<Category>()
                    .Where(nameof(Category.Slug), Is.EqualTo, categorySlug).Execute();

                if (category != null)
                {
                    query.Where(nameof(Post.CategoryId), Is.EqualTo, category.Id);
                }
            }

            var posts = await query.Order(nameof(Post.Views), Order.Descending).Execute();

            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _query.GetOne<Post>().Where(nameof(Post.Id), Is.EqualTo, postId).Execute();
            return post;
        }

        public async Task<Post> GetPost(string postSlug)
        {
            var post = await _query.GetOne<Post>()
                .Where(nameof(Post.Slug), Is.EqualTo, postSlug).Execute();
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts(string userName)
        {
            var user = await _query.GetOne<User>()
                .Where(nameof(User.UserName), Is.EqualTo, userName).Execute();

            var posts = await _query.GetMany<Post>()
                .Where(nameof(Post.UserId), Is.EqualTo, user.Id).Execute();

            return posts;
        }

        public async Task<Post> CreatePost(Post post)
        {
            var id = await _query.Insert<Post>().Values(post, nameof(Post.Id)).Execute();
            return await GetPost(id);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            await _query.Update<Post>()
                .Set(nameof(Post.UserId), post.UserId)
                .Set(nameof(Post.Title), post.Title)
                .Set(nameof(Post.Content), post.Content)
                .Where(nameof(Post.Id), Is.EqualTo, post.Id)
                .Execute();

            var updatedEntity = await _query.GetOne<Post>()
                .Where(nameof(Post.Id), Is.EqualTo, post.Id).Execute();
            return updatedEntity;
        }

        public async Task<int> DeletePost(int postId)
        {
            int id = await _query.DeleteOne<Post>().Where(nameof(Post.Id), Is.EqualTo, postId).Execute();
            return id;
        }

        // Consider: Need PostTagService?
        public async Task<IEnumerable<PostTag>> GetPostTags(int postId)
        {
            var tags = await _query.GetMany<PostTag>()
                .Where(nameof(PostTag.PostId), Is.EqualTo, postId).Execute();

            return tags;
        }

        public async Task<IEnumerable<Post>> GetPostsByTag(string tag)
        {
            var tags = await _query.GetMany<PostTag>()
                .Where(nameof(PostTag.Tag), Is.EqualTo, tag).Execute();

            var posts = await _query.GetMany<Post>()
                .Where(nameof(Post.Id), Is.In, tags.Select(t => t.PostId))
                .Execute();

            return posts;
        }

        public async Task<IEnumerable<string>> GetPopularTags(int count)
        {
            // todo: optimize this through custom query or enhance query builder.
            var postTags = await _query.GetMany<PostTag>().Execute();

            var groups = postTags.GroupBy((tag) => tag.Tag).OrderByDescending(group => group.Count());

            var tags = groups.Select(group => group.Key);
            return tags;
        }

        public async Task IncrementViews(int postId)
        {
            var post = await GetPost(postId);

            if (post == null)
                return;

            await _query.Update<Post>()
                .Set(nameof(Post.Views), ++post.Views)
                .Where(nameof(Post.Id), Is.EqualTo, postId)
                .Execute();
        }
    }
}
