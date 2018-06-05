﻿using Peppermint.Blog.Entities;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.Blog.Services
{
    public class PostService : EntityService
    {
        private IQueryBuilder _query;

        public PostService(IQueryBuilder query) : base(query)
        {
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _query.GetMany<Post>().Execute();
            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _query.GetOne<Post>().Where(nameof(Post.Id), Is.EqualTo, postId).Execute();
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts(string userName)
        {
            var user = await _query.GetOne<User>()
                .Where(nameof(User.UserName), Is.EqualTo, userName).Execute();

            var posts = await _query.GetMany<Post>()
                .Where(nameof(Post.UserId), Is.EqualTo, user.Id).Execute();

            return posts;
        }

        public async Task<Post> CreatePost(Post post)
        {
            // Possibly simplify this by passing T to the data accessor and reflecting over it?
            var id = await _query.Insert<Post>()
                .Value(nameof(Post.UserId), post.UserId)
                .Value(nameof(Post.Title), post.Title)
                .Value(nameof(Post.Content), post.Content)
                .Execute();

            return await GetPost(id);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            await _query.Update<Post>()
                .Set(nameof(Post.UserId), post.UserId)
                .Set(nameof(Post.Title), post.Title)
                .Set(nameof(Post.Content), post.Content)
                .Where(nameof(Post.Id), Is.EqualTo, post.Id)
                .Execute();

            var updatedEntity = await _query.GetOne<Post>()
                .Where(nameof(Post.Id), Is.EqualTo, post.Id).Execute();
            return updatedEntity;
        }

        public async Task<int> DeletePost(int postId)
        {
            // todo : implement delete in builder

            var conditions = new List<QueryCondition> {
                new QueryCondition(nameof(Post.Id), Is.EqualTo, postId)
            };

            var deletedId = await _postData.Delete(conditions);
            return deletedId;
        }

    }
}
