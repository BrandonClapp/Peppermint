using Peppermint.Core.Data;
using Peppermint.Core.Entities;

namespace Peppermint.Commerce.Entities
{
    [DataLocation("store.Product")]
    public class Product : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
