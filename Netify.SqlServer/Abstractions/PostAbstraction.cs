using Netify.Common.Data;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.SqlServer
{
    public class PostAbstraction : SqlServerDataAbstraction, IPostDataAbstraction
    {
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await GetMany<Post>("SELECT * FROM Posts");
            return posts;
        }
    }
}
