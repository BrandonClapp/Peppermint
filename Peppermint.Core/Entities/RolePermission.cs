using Peppermint.Core.Data;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.RolePermissions")]
    public class RolePermission : DataEntity
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public bool Permit { get; set; }

        // NULL = Not needed
        // "ALL" = All
        public string GroupEntityId { get; set; }
    }
}
