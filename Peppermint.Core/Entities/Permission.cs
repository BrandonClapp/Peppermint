using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Permissions")]
    public class Permission : DataEntity
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
    }
}
