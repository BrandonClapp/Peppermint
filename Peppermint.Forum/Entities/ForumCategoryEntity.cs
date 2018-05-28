using Peppermint.Core.Entities;

namespace Peppermint.Forum.Entities
{
    public class ForumCategoryEntity : DataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string GetDataLocation()
        {
            return $"{ModuleSettings.Schema}.Categories";
        }
    }
}
