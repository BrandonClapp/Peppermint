using Peppermint.Blog.Entities;
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
        private IDataAccessor<Post> _postData;
        private IDataAccessor<User> _userData;

        // todo: authorization

        public PostService(IDataAccessor<Post> postData, IDataAccessor<User> userData)
        {
            _postData = postData;
            _userData = userData;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var postEntities = await _postData.GetAll();
            return postEntities;
        }

        public async Task<Post> GetPost(int postId)
        {
            var postEntity = await _postData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(Post.Id), Is.EqualTo, postId)
            });

            return postEntity;
        }

        public async Task<IEnumerable<Post>> GetPosts(string userName)
        {
            var user = await _userData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(User.UserName), Is.EqualTo, userName)
            });

            var postEntities = await _postData.GetMany(new List<QueryCondition>() {
                new QueryCondition(nameof(Post.UserId), Is.EqualTo, user.Id)
            });

            var posts = await Task.WhenAll(postEntities.Select(async p => await GetPost(p.Id)));
            return posts;
        }

        public async Task<Post> CreatePost(Post postEntity)
        {
            // Possibly simplify this by passing T to the data accessor and reflecting over it?

            var parameters = new List<InsertQueryParameter>() {
                new InsertQueryParameter(nameof(Post.UserId), postEntity.UserId),
                new InsertQueryParameter(nameof(Post.Title), postEntity.Title),
                new InsertQueryParameter(nameof(Post.Content), postEntity.Content),
            };

            var post = await _postData.Create(parameters);

            return await GetPost(post.Id);
        }

        public async Task<Post> UpdatePost(Post postEntity)
        {
            var parameters = new List<UpdateQueryParameter> {
                new UpdateQueryParameter(nameof(Post.Id), UpdateQueryParameterType.Identity, postEntity.Id),
                new UpdateQueryParameter(nameof(Post.UserId), UpdateQueryParameterType.Value, postEntity.UserId),
                new UpdateQueryParameter(nameof(Post.Title), UpdateQueryParameterType.Value, postEntity.Title),
                new UpdateQueryParameter(nameof(Post.Content), UpdateQueryParameterType.Value, postEntity.Content)
            };

            var updatedEntity = await _postData.Update(parameters);
            return updatedEntity;
        }

        public async Task<int> DeletePost(int postId)
        {
            var conditions = new List<QueryCondition> {
                new QueryCondition(nameof(Post.Id), Is.EqualTo, postId)
            };

            var deletedId = await _postData.Delete(conditions);
            return deletedId;
        }

    }
}
