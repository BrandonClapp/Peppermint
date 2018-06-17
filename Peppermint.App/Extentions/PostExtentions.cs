using Peppermint.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.Extentions
{
    public static class PostExtentions
    {
        public static async Task<Post> ToPost(this Blog.Entities.Post entity)
        {
            var post = new Post()
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                CategoryId = entity.CategoryId,
                Content = entity.Content,
                UserId = entity.UserId,
                Created = entity.Created,
                Updated = entity.Updated,
                Likes = entity.Likes,
                Views = entity.Views
            };

            post.Html = await entity.GetHtml();
            post.Thumbnail = await entity.GetThumbnail();
            post.Banner = await entity.GetBanner();
            post.Category = await entity.GetCategory();
            post.User = await entity.GetUser();

            return post;
        }

        public static async Task<IEnumerable<Post>> ToPosts(this IEnumerable<Blog.Entities.Post> entities)
        {
            return await Task.WhenAll(entities.Select(async ent => await ToPost(ent)));
        }
    }
}
