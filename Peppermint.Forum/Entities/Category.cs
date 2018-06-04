using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Forum.Authorization;

namespace Peppermint.Forum.Entities
{
    [DataLocation("forum.Categories")]
    public class Category : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
