using System.Collections.Generic;

namespace Peppermint.App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
