using Netify.Common.Data;
using Netify.Common.Entities;
using System.Collections.Generic;
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

        public async Task<IEnumerable<PostEntity>> GetPosts()
        {
            var postEntities = await _postData.GetAll();
            return postEntities;
        }

        public async Task<PostEntity> GetPost(int postId)
        {
            var postEntity = await _postData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(PostEntity.Id), ConditionType.Equals, postId)
            });

            return postEntity;
        }

        public async Task<IEnumerable<PostEntity>> GetPosts(string userName)
        {
            var user = await _userData.GetOne(new List<QueryCondition>() {
                new QueryCondition(nameof(UserEntity.UserName), ConditionType.Equals, userName)
            });

            var postEntities = await _postData.GetMany(new List<QueryCondition>() {
                new QueryCondition(nameof(PostEntity.UserId), ConditionType.Equals, user.Id)
            });

            var posts = await Task.WhenAll(postEntities.Select(async p => await GetPost(p.Id)));
            return posts;
        }

        public async Task<PostEntity> CreatePost(PostEntity postEntity)
        {
            // Possibly simplify this by passing T to the data accessor and reflecting over it?

            var parameters = new List<InsertQueryParameter>() {
                new InsertQueryParameter(nameof(PostEntity.UserId), postEntity.UserId),
                new InsertQueryParameter(nameof(PostEntity.Title), postEntity.Title),
                new InsertQueryParameter(nameof(PostEntity.Content), postEntity.Content),
            };

            var post = await _postData.Create(parameters);

            return await GetPost(post.Id);
        }

        public async Task<PostEntity> UpdatePost(PostEntity postEntity)
        {
            var parameters = new List<UpdateQueryParameter> {
                new UpdateQueryParameter(nameof(PostEntity.Id), UpdateQueryParameterType.Identity, postEntity.Id),
                new UpdateQueryParameter(nameof(PostEntity.UserId), UpdateQueryParameterType.Value, postEntity.UserId),
                new UpdateQueryParameter(nameof(PostEntity.Title), UpdateQueryParameterType.Value, postEntity.Title),
                new UpdateQueryParameter(nameof(PostEntity.Content), UpdateQueryParameterType.Value, postEntity.Content)
            };

            var updatedEntity = await _postData.Update(parameters);
            return updatedEntity;
        }

        public async Task<int> DeletePost(int postId)
        {
            var conditions = new List<QueryCondition> {
                new QueryCondition(nameof(PostEntity.Id), ConditionType.Equals, postId)
            };

            var deletedId = await _postData.Delete(conditions);
            return deletedId;
        }

    }
}
