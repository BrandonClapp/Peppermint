﻿using Netify.Common.Data;
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
        private IDataAccessor<PostEntity> _postData;
        private IDataAccessor<UserEntity> _userData;

        public PostService(IDataAccessor<PostEntity> postData, IDataAccessor<UserEntity> userData)
        {
            _postData = postData;
            _userData = userData;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var postEntities = await _postData.GetAll();
            var userEntities = await _userData.GetAll();

            var posts = postEntities.Select(post =>
            {
                var userEntity = userEntities.FirstOrDefault(user => user.Id == post.UserId);
                return Construct(post, userEntity);
            });

            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var postEntity = await _postData.GetOne(postId);
            var userEntity = await _userData.GetOne(postEntity.UserId);

            var post = Construct(postEntity, userEntity);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts(string userName)
        {
            var user = await _userData.GetOne(new List<QueryCondition>()
            {
                new QueryCondition("UserName", ConditionType.Equals, userName)
            });

            var postEntities = await _postData.GetMany(new List<QueryCondition>()
            {
                new QueryCondition("UserId", ConditionType.Equals, user.Id)
            });

            var posts = await Task.WhenAll(postEntities.Select(async p => await GetPost(p.Id)));
            return posts;
        }

        public async Task<Post> CreatePost(PostEntity postEntity)
        {
            var post = await _postData.Create(postEntity);
            return await GetPost(post.Id);
        }

        public async Task<Post> UpdatePost(PostEntity postEntity)
        {
            var updatedEntity = await _postData.Update(postEntity);
            return await GetPost(updatedEntity.Id);
        }

        public async Task<int> DeletePost(int postId)
        {
            var deletedId = await _postData.Delete(postId);
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
