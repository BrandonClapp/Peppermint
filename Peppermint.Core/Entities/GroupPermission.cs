using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.GroupPermissions")]
    public class GroupPermission : DataEntity
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int UserGroupId { get; set; }
        public bool Permit { get; set; }

        // NULL = Not needed
        // "ALL" = All
        public string GroupEntityId { get; set; }

    }
}
