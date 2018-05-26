using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.Common.Entities
{
    public class PostEntity : DataEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
