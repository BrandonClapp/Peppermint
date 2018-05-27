using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IPostDataAbstraction
    {
        Task<PostEntity> GetPost(int postId);
        Task<PostEntity> GetPost(IEnumerable<QueryCondition> filters);

        Task<IEnumerable<PostEntity>> GetPosts();
        Task<IEnumerable<PostEntity>> GetPosts(IEnumerable<QueryCondition> filters);

        Task<PostEntity> CreatePost(PostEntity post);
        Task<PostEntity> UpdatePost(PostEntity post);
        Task<int> DeletePost(int postId);
    }
}
