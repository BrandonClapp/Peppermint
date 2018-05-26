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
