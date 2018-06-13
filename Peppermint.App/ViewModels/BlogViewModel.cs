using Peppermint.App.Models;
using System.Collections.Generic;

namespace Peppermint.App.ViewModels
{
    public class BlogViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
