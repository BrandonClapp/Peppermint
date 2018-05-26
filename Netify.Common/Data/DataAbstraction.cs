using Netify.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netify.Common.Data
{
    public abstract class DataAbstraction
    {
        public abstract Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null);

        public abstract Task<T> GetSingle<T>(string query, object parameters = null);
        public abstract Task<T> GetSingleOrDefault<T>(string query, object parameters = null);

        public abstract Task<T> GetFirst<T>(string query, object parameters = null);
        public abstract Task<T> GetFirstOrDefault<T>(string query, object parameters = null);

        public abstract Task<int> AddItem(string query, object parameters);
    }
}
