using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserGroupPermissions")]
    public class UserGroupPermissionEnitity : DataEntity
    {
        public int Id { get; set; }
        public int PermissionTypeId { get; set; }
        public int UserGroupId { get; set; }
        public bool CanPerformAction { get; set; }

        // NULL qualifies as "All"
        public int? EntityQualifier { get; set; }
    }
}
