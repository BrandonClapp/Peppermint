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

        private Post Construct(PostEntity postEntity, UserEntity userEntity)
        {
            if (postEntity == null)
                throw new ResourceNotFoundException();

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
