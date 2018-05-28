using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Core.Entities
{
    public class UserEntity : DataEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public override string GetDataLocation()
        {
            return $"{ModuleSettings.Schema}.Users";
        }
    }
}
