using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Forum.Entities
{
    public class ForumCategoryEntity : DataEntity
    {
        public override string DataLocation { get; set; } = "forum.Categories";

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
