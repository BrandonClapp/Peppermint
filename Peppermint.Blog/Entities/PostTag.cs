using Peppermint.Core.Data;
using Peppermint.Core.Entities;

namespace Peppermint.Blog.Entities
{
    [DataLocation("blog.PostTags")]
    public class PostTag : DataEntity
    {
        public int PostId { get; set; }
        public string Tag { get; set; }
    }
}
