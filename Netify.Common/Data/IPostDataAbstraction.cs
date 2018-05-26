using Netify.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IPostDataAbstraction
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> AddPost(Post post);
    }
}
