using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public abstract class DataAbstraction
    {
        public abstract Task<IEnumerable<T>> GetMany<T>(string query, object parameters);
    }
}
