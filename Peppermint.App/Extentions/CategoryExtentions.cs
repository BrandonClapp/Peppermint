using Peppermint.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.Extentions
{
    public static class CategoryExtentions
    {
        public static async Task<Category> ToCategory(this Blog.Entities.Category category)
        {
            var cat = await Task.FromResult(new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            });

            return cat;
        }
    }
}
