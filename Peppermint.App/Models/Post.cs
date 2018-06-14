using Peppermint.App.Extentions;
using Peppermint.Blog.Entities;
using Peppermint.Core.Entities;
using System;

namespace Peppermint.App.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public string Thumbnail { get; set; }
        public string Banner { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }

        public string GetExcerpt()
        {
            return Content.Truncate(400);
        }
    }
}
