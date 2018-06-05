using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Roles")]
    public class Role : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
