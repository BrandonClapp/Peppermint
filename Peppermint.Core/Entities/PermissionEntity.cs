using Peppermint.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Permissions")]
    public class PermissionEntity : DataEntity
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Group { get; set; }
        public string Permission { get; set; }
    }
}
