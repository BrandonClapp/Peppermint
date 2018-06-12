using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Forum.Entities
{
    [DataLocation("forum.Categories")]
    public class Category : DataEntity
    {
        //private readonly CategoryService _categoryService;
        //public Category(CategoryService categoryService)
        //{
        //    _categoryService = categoryService;
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        //public async Task<IEnumerable<Post>> GetPosts()
        //{
        //    var posts = _categoryService.
        //}
    }
}
