using Dapper;
using Netify.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Netify.SqlServer
{
    public class SqlServerDataAbstraction : DataAbstraction
    {
        private string _connString;

        public SqlServerDataAbstraction(string connString)
        {
            _connString = connString;
        }

        public async override Task<IEnumerable<T>> GetMany<T>(string query, object parameters)
        {
            var many = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryAsync<T>(query, parameters, trans);
                return result;
            });

            return many;
        }

        public async override Task<T> GetSingle<T>(string query, object parameters)
        {
            var single = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleAsync<T>(query, parameters, trans);
                return result;
            });

            return single;
        }

        public async override Task<T> GetSingleOrDefault<T>(string query, object parameters)
        {
            var singleOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleOrDefaultAsync<T>(query, parameters, trans);
                return result;
            });

            return singleOrDefault;
        }

        public async override Task<T> GetFirst<T>(string query, object parameters)
        {
            var first = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstAsync<T>(query, parameters, trans);
                return result;
            });

            return first;
        }

        public async override Task<T> GetFirstOrDefault<T>(string query, object parameters)
        {
            var firstOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstOrDefaultAsync<T>(query, parameters, trans);
                return result;
            });

            return firstOrDefault;
        }

        private async Task<T> BootstrapCommand<T>(Func<SqlConnection, SqlTransaction, Task<T>> command)
        {
            using (var conn = new SqlConnection(_connString))
            using (var trans = conn.BeginTransaction())
            {
                try
                {
                    var result = await command(conn, trans);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    // todo: logging
                    throw ex;
                }
            }
        }

        private async Task<IEnumerable<T>> BootstrapCommand<T>(Func<SqlConnection, SqlTransaction, Task<IEnumerable<T>>> command)
        {
            return await BootstrapCommand<IEnumerable<T>>(command);
        }
    }
}
