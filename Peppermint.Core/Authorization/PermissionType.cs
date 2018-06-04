using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Authorization
{
    public abstract class PermissionType
    {
        public abstract string PermissionGroup { get; }
        public abstract string Module { get; }

        public PermissionType(string value) { Value = value; }
        public string Value { get; set; }
    }
}
