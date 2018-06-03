using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserGroups")]
    public partial class UserGroup : DataEntity
    {
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
    }
}
