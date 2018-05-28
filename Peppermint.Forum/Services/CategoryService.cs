using Peppermint.Core.Data;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Services
{
    public class CategoryService : EntityService
    {
        private readonly IDataAccessor<CategoryEntity> _forumCategoryData;
        public CategoryService(IDataAccessor<CategoryEntity> forumCategoryData)
        {
            _forumCategoryData = forumCategoryData;
        }

        public async Task<CategoryEntity> GetForumCategory(int id)
        {
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

            return category;
        }
    }
}
