using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.UserRoles")]
    public class UserRole : DataEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
