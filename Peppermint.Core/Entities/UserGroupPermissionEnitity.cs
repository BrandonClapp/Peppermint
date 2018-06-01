using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserGroupPermissions")]
    public partial class UserGroupPermissionEnitity : DataEntity
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
