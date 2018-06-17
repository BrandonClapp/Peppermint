namespace Peppermint.App.Models
{
    public class Tag
    {
        public Tag(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
