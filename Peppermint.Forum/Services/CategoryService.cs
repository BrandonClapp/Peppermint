using Peppermint.Core.Data;
using Peppermint.Core.Services;
using Peppermint.Forum.Authorization;
using Peppermint.Forum.Entities;
using System;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class CategoryService : EntityService
    {
        private CategoryAuthorizationService _catAuth;

        public CategoryService(IQueryBuilder query, CategoryAuthorizationService catAuth)
            : base (query)
        {
            _query = query;
            _catAuth = catAuth;
        }

        public async Task<Category> GetForumCategory(int id)
        {
            var canView = await _catAuth.CanViewCategory(id);

            if (!canView)
                throw new Exception("Unauthorized.");

            var category = await _query.GetOne<Category>()
                .Where(nameof(Category.Id), Is.EqualTo, id).Execute();

            return category;
        }

        public async Task<Category> GetForumCategory(string name)
        {
            var category = await _query.GetOne<Category>()
                .Where(nameof(Category.Name), Is.EqualTo, name).Execute();

            // ensure not null

            var canView = await _catAuth.CanViewCategory(category.Id);

            if (!canView)
                throw new Exception("Unauthorized.");

            return category;
        }
    }
}
