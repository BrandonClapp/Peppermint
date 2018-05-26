using Netify.Common.Data;
using Netify.Common.Entities;
using Netify.Common.Models;
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

        public async Task<Post> GetPost(int postId)
        {
            var postEntity = await _postData.GetPost(postId);
            var userEntity = await _userData.GetUser(postEntity.UserId);

            var post = Construct(postEntity, userEntity);
            return post;
        }

        private Post Construct(PostEntity postEntity, UserEntity userEntity)
        {
            return new Post()
            {
                Id = postEntity.Id,
                Title = postEntity.Title,
                Content = postEntity.Content,
                User = new User()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    UserName = userEntity.UserName
                }
            };
        }
    }
}
