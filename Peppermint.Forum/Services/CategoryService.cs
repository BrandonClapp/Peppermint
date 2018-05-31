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
        private readonly IDataAccessor<CategoryEntity> _forumCategoryData;
        private CategoryAuthorizationService _catAuth;

        public CategoryService(IDataAccessor<CategoryEntity> forumCategoryData, CategoryAuthorizationService catAuth)
        {
            _forumCategoryData = forumCategoryData;
            _catAuth = catAuth;
        }

        public async Task<CategoryEntity> GetForumCategory(int id)
        {
            var canView = await _catAuth.CanViewCategory(id);

            if (!canView)
                throw new Exception("Unauthorized.");

            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(CategoryEntity.Id), ConditionType.Equals, id)
            });

            return category;
        }

        public async Task<CategoryEntity> GetForumCategory(string name)
        {
            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(CategoryEntity.Name), ConditionType.Equals, name)
            });

            var canView = await _catAuth.CanViewCategory(category.Id);

            if (!canView)
                throw new Exception("Unauthorized.");

            return category;
        }
    }
}
