using Peppermint.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Roles")]
    public class Role : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
