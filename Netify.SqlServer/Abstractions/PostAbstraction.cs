using Netify.Common.Data;
using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.SqlServer
{
    public class PostAbstraction : IPostDataAbstraction
    {
        private SqlServerDataAbstraction _data;

        public PostAbstraction(SqlServerDataAbstraction data)
        {
            _data = data;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _data.GetMany<Post>("SELECT * FROM Posts");
            return posts;
        }
    }
}
