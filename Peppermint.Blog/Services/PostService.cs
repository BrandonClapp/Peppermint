using Peppermint.Blog.Entities;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Post>> GetRecentPosts(int pageSize = 15, int page = 1, string categorySlug = null)
        {
            var query = _query.GetMany<Post>();

            if (!string.IsNullOrEmpty(categorySlug))
            {
                var category = await _query.GetOne<Category>()
                    .Where(nameof(Category.Slug), Is.EqualTo, categorySlug).Execute();

                if (category != null)
                {
                    query.Where(nameof(Post.CategoryId), Is.EqualTo, category.Id);
                }
            }

            var posts = await query.Order(nameof(Post.Id), Order.Descending)
                .Pagination(pageSize, page)
                .Execute();

            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _query.GetOne<Post>().Where(nameof(Post.Id), Is.EqualTo, postId).Execute();
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

        public async Task<IEnumerable<PostTag>> GetPostTags(int postId)
        {
            var tags = await _query.GetMany<PostTag>()
                .Where(nameof(PostTag.PostId), Is.EqualTo, postId).Execute();

            return tags;
        }


    }
}
