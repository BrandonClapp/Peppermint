namespace Peppermint.App.Models
{
    public class Pagination
    {
        public bool CanGoPrevPage { get; set; }
        public bool CanGoNextPage { get; set; }
        public int CurrentPage { get; set; }
        public int MinPage { get; set; }
        public int MaxPage { get; set; }
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
    }
}
