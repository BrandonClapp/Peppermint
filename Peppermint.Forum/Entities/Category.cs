using Peppermint.Core.Data;
using Peppermint.Core.Entities;

namespace Peppermint.Forum.Entities
{
    [DataLocation("forum.Categories")]
    public class Category : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
