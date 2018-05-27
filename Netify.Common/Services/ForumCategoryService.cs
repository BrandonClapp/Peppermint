using Netify.Common.Data;
using Netify.Common.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Services
{
    public class ForumCategoryService : EntityService
    {
        private readonly IDataAccessor<ForumCategoryEntity> _forumCategoryData;
        public ForumCategoryService(IDataAccessor<ForumCategoryEntity> forumCategoryData)
        {
            _forumCategoryData = forumCategoryData;
        }

        public async Task<ForumCategoryEntity> GetForumCategory(int id)
        {
            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(ForumCategoryEntity.Id), ConditionType.Equals, id)
            });

            return category;
        }

        public async Task<ForumCategoryEntity> GetForumCategory(string name)
        {
            var category = await _forumCategoryData.GetOne(new List<QueryCondition> {
                new QueryCondition(nameof(ForumCategoryEntity.Name), ConditionType.Equals, name)
            });

            return category;
        }
    }
}
