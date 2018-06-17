using Peppermint.Blog.Entities;
using Peppermint.Blog.Utilities;
using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Blog.Services
{
    public class TagService : EntityService
    {
        public TagService(IQueryBuilder query) : base(query)
        {
        }

        public async Task<int> GetTotalPosts(string tagSlug)
        {
            var tagName = Slug.Reverse(tagSlug);
            var tag = await _query.GetMany<PostTag>()
                .Where(nameof(PostTag.Tag), Is.EqualTo, tagName).Execute();

            return tag.Count();
        }
    }
}
