using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public interface IPostDataAbstraction
    {
        Task<Post> GetPosts();
    }
}
