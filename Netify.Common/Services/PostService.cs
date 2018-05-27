using Netify.Common.Data;
using Netify.Common.Entities;
using Netify.Common.Exceptions;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Netify.Common.Services
{
    public class PostService
    {
        private readonly IPostDataAbstraction _postData;
        private readonly IUserDataAbstraction _userData;

        public PostService(IPostDataAbstraction postData, IUserDataAbstraction userData)
        {
            _postData = postData;
            _userData = userData;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var postEntities = await _postData.GetPosts();
            var userEntities = await _userData.GetUsers();

            var posts = postEntities.Select(post =>
            {
                var userEntity = userEntities.FirstOrDefault(user => user.Id == post.UserId);
                return Construct(post, userEntity);
            });

            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var postEntity = await _postData.GetPost(postId);
            var userEntity = await _userData.GetUser(postEntity.UserId);

            var post = Construct(postEntity, userEntity);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts(string userName)
        {
            // needs get user by username method
            var user = (await _userData.GetUsers()).First(u => u.UserName == userName);

            var postEntities = await _postData.GetPosts(new List<QueryCondition>()
            {
                new QueryCondition("UserId", ConditionType.Equals, user.Id)
            });

            var posts = await Task.WhenAll(postEntities.Select(async p => await GetPost(p.Id)));
            return posts;
        }

        public async Task<Post> CreatePost(PostEntity postEntity)
        {
            var post = await _postData.CreatePost(postEntity);
            return await GetPost(post.Id);
        }

        public async Task<Post> UpdatePost(PostEntity postEntity)
        {
            var updatedEntity = await _postData.UpdatePost(postEntity);
            return await GetPost(updatedEntity.Id);
        }

        public async Task<int> DeletePost(int postId)
        {
            var deletedId = await _postData.DeletePost(postId);
            return deletedId;
        }

        private Post Construct(PostEntity postEntity, UserEntity userEntity)
        {
            if (postEntity == null)
                return null;

            var post = new Post()
            {
                Id = postEntity.Id,
                Title = postEntity.Title,
                Content = postEntity.Content
            };

            if (userEntity != null)
            {
                post.User = new User()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    UserName = userEntity.UserName
                };
            }

            return post;
        }
    }
}
