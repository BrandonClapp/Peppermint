using Peppermint.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.PermissionTypes")]
    public class PermissionTypeEntity : DataEntity
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PermissionCategory { get; set; }
        public string PermissionName { get; set; }
        public string EntityType { get; set; }
        public string PermissionDescription { get; set; }
    }
}
