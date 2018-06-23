using Peppermint.App.Extentions;
using Peppermint.App.Models;
using Peppermint.Blog.Services;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Peppermint.App.ViewModels.Blog
{
    public class BlogPostViewModel : BlogViewModel
    {
        private readonly PostService _postService;

        public BlogPostViewModel(UserFactory userFactory, BlogSidebarViewModel sidebar,
            PostService postService): base (userFactory, sidebar)
        {
            _postService = postService;
        }

        public Post Post { get; set; }
        public IEnumerable<Post> RelatedPosts { get; set; }

        public async Task<BlogPostViewModel> Build(string postSlug)
        {
            await base.Build();

            var post = await BuildPost(postSlug);
            var relatedPosts = await BuildRelatedPosts(post, 3);

            Post = post;
            RelatedPosts = relatedPosts;

            return this;
        }

        private async Task<Post> BuildPost(string postSlug)
        {
            var entity = await _postService.GetPost(postSlug);
            await entity.IncrementViews();
            return await entity.ToPost();
        }

        private async Task<IEnumerable<Post>> BuildRelatedPosts(Post post, int count)
        {
            var tags = await _postService.GetPostTags(post.Id);

            var relatedPosts = new List<Peppermint.Blog.Entities.Post>();
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
                    return (await relatedPosts.ToPosts()).Take(count);
                }
            }

            // second priorty, match on like words, prefering longer words first.

            //var titleWords = post.Title.Split(" ");
            //// order by length, descending.
            //Array.Sort(titleWords, (x, y) => y.Length.CompareTo(x.Length));

            //foreach (var word in titleWords)
            //{

            //}

            return (await relatedPosts.ToPosts()).Take(count);
        }

        
    }
}
