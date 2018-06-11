using Peppermint.Blog.Entities;
using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Blog.Services
{
    public class CategoryService : EntityService
    {
        public CategoryService(IQueryBuilder query) : base(query)
        {
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _query.GetOne<Category>().Where(nameof(Category.Id), Is.EqualTo, id)
                .Execute();

            return category;
        }

        public async Task<Category> GetCategory(string name)
        {
            var category = await _query.GetOne<Category>().Where(nameof(Category.Name), Is.EqualTo, name)
                .Execute();

            return category;
        }

        public async Task<IEnumerable<Post>> GetPosts(int id)
        {
            var posts = await _query.GetMany<Post>().Where(nameof(Post.CategoryId), Is.EqualTo, id)
                .Execute();

            return posts;
        }
    }
}
