using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Services
{
    [DataLocation("core.UserRoles")]
    public class UserRoleEntity : DataEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
