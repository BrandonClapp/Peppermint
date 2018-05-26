using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.Common.Entities
{
    public class UserEntity : DataEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
