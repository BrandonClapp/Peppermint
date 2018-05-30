using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserUserGroups")]
    public class UserUserGroupEntity : DataEntity
    {
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
    }
}
