using Peppermint.Core.Authorization;

namespace Peppermint.Core.Permissions
{
    public class AdminPermissions : PermissionType
    {
        public AdminPermissions(string value) : base(value) { }

        public override string Module { get; } = ModuleSettings.Schema;
        public override string PermissionGroup { get; } = "Admin";

        public static AdminPermissions CanAccessAdmin { get { return new AdminPermissions(nameof(CanAccessAdmin)); } }
    }
}
