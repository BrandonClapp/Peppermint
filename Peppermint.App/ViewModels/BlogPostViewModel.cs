using Peppermint.App.Models;
using Peppermint.Blog.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels
{
    public class BlogPostViewModel : ViewModel
    {
        private readonly PostService _postService;
        public BlogPostViewModel(PostService postService)
        {
            _postService = postService;
        }

        public Post Post { get; set; }
        public IEnumerable<Post> RelatedPosts { get; set; }

        public async Task<BlogPostViewModel> Build(string postSlug)
        {
            var post = await BuildPost(postSlug);
            var relatedPosts = await BuildRelatedPosts(post, 3);

            Post = post;
            RelatedPosts = relatedPosts;

            return this;
        }

        private async Task<Post> EntityToPost(Blog.Entities.Post entity)
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
                Updated = entity.Updated
            };

            post.Html = await entity.GetHtml();
            post.Thumbnail = await entity.GetThumbnail();
            post.Banner = await entity.GetBanner();
            post.Category = await entity.GetCategory();
            post.User = await entity.GetUser();

            return post;
        }

        private async Task<IEnumerable<Post>> EntitiesToPosts(IEnumerable<Blog.Entities.Post> entities)
        {
            return await Task.WhenAll(entities.Select(async ent => await EntityToPost(ent)));
        }

        private async Task<Post> BuildPost(string postSlug)
        {
            var entity = await _postService.GetPost(postSlug);

            return await EntityToPost(entity);
        }

        private async Task<IEnumerable<Post>> BuildRelatedPosts(Post post, int count)
        {
            var tags = await _postService.GetPostTags(post.Id);

            var relatedPosts = new List<Blog.Entities.Post>();
            var matched = 0;

            // first priority, match on same tags.
            foreach (var tag in tags)
            {
                var tagPosts = await _postService.GetPostsByTag(tag.Tag);
                tagPosts = tagPosts.Where(p => post.Id != p.Id);
                relatedPosts.AddRange(tagPosts);
                matched += tagPosts.Count();

                if (matched >= count)
                {
                    return (await EntitiesToPosts(relatedPosts)).Take(count);
                }
            }

            // second priorty, match on like words, prefering longer words first.

            //var titleWords = post.Title.Split(" ");
            //// order by length, descending.
            //Array.Sort(titleWords, (x, y) => y.Length.CompareTo(x.Length));

            //foreach (var word in titleWords)
            //{

            //}

            return (await EntitiesToPosts(relatedPosts)).Take(count);
        }
    }
}
