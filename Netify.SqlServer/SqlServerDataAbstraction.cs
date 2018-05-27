using Dapper;
using Netify.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netify.SqlServer
{
    public class SqlServerDataAbstraction : DataAbstraction
    {
        private readonly string _connString;

        public SqlServerDataAbstraction(string connString)
        {

            _connString = connString;
        }

        public async override Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null)
        {
            var many = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryAsync<T>(query, parameters, trans);
                return result;
            });

            return many;
        }

        public async override Task<T> GetSingle<T>(string query, object parameters = null)
        {
            var single = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleAsync<T>(query, parameters, trans);
                return result;
            });

            return single;
        }

        public async override Task<T> GetSingleOrDefault<T>(string query, object parameters = null)
        {
            var singleOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleOrDefaultAsync<T>(query, parameters, trans);
                return result;
            });

            return singleOrDefault;
        }

        public async override Task<T> GetFirst<T>(string query, object parameters = null)
        {
            var first = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstAsync<T>(query, parameters, trans);
                return result;
            });

            return first;
        }

        public async override Task<T> GetFirstOrDefault<T>(string query, object parameters = null)
        {
            var firstOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstOrDefaultAsync<T>(query, parameters, trans);
                return result;
            });

            return firstOrDefault;
        }

        public async override Task UpdateItem(string query, object parameters)
        {
            await BootstrapCommand<int>(async (conn, trans) =>
            {
                await conn.ExecuteAsync(query, parameters, trans);
                return 0;
            });
        }

        public async Task<int> DeleteItem(string query, object parameters)
        {
            return await AddRemoveItem(query, parameters);
        }

        public async override Task<int> AddItem(string query, object parameters)
        {
            return await AddRemoveItem(query, parameters);
        }

        private async Task<int> AddRemoveItem(string query, object parameters)
        {
            var item = await BootstrapCommand<int>(async (conn, trans) =>
            {
                query += "\r\n SELECT CAST(SCOPE_IDENTITY() as int)";
                var addedId = (await conn.QueryAsync<int>(query, parameters, trans)).Single();
                return addedId;
            });

            return item;
        }

        private async Task<T> BootstrapCommand<T>(Func<SqlConnection, SqlTransaction, Task<T>> command)
        {
            // todo: sql connection pooling?
            using (var conn = new SqlConnection(_connString))
            {
                try
                {
                    await conn.OpenAsync();
                }
                catch (SqlException ex)
                {
                    // The login failed or database could not be opened.
                    // todo: logging
                    throw ex;
                }

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
        }

        private async Task<IEnumerable<T>> BootstrapCommand<T>(Func<SqlConnection, SqlTransaction, Task<IEnumerable<T>>> command)
        {
            return await BootstrapCommand<IEnumerable<T>>(command);
        }
    }
}
