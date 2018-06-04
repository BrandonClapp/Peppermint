using Peppermint.Core.Data;
using Peppermint.Core.Services;
using Peppermint.Forum.Authorization;
using Peppermint.Forum.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class CategoryService : EntityService
    {
        private readonly IDataAccessor<Category> _forumCategoryData;
        private CategoryAuthorizationService _catAuth;

        public CategoryService(IDataAccessor<Category> forumCategoryData, CategoryAuthorizationService catAuth)
        {
            _forumCategoryData = forumCategoryData;
            _catAuth = catAuth;
        }

        public async Task<Category> GetForumCategory(int id)
        {
            var canView = await _catAuth.CanViewCategory(id);

            if (!canView)
                throw new Exception("Unauthorized.");

            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(Category.Id), Is.EqualTo, id)
            });

            return category;
        }

        public async Task<Category> GetForumCategory(string name)
        {
            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(Category.Name), Is.EqualTo, name)
            });

            var canView = await _catAuth.CanViewCategory(category.Id);

            if (!canView)
                throw new Exception("Unauthorized.");

            return category;
        }
    }
}
