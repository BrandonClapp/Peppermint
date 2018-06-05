using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserRoles")]
    public class UserRole : DataEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
