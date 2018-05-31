using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserUserGroups")]
    public partial class UserUserGroupEntity : DataEntity
    {
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
    }
}
