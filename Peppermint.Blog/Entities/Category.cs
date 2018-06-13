using Peppermint.Blog.Services;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Blog.Entities
{
    [DataLocation("blog.Categories")]
    public class Category : DataEntity
    {
        private CategoryService _categoryService;
        public Category(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _categoryService.GetPosts(Id);
        }
    }
}
