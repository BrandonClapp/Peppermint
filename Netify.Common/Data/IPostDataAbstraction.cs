using Netify.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IPostDataAbstraction
    {
        Task<PostEntity> GetPost(int postId);
        Task<IEnumerable<PostEntity>> GetPosts();
        Task<PostEntity> AddPost(PostEntity post);
    }
}
