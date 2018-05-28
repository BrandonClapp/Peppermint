using Dapper;
using Peppermint.Core.Data;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peppermint.SqlServer
{
    public class SqlServerDataAbstraction : IDataAbstraction
    {
        private readonly string _connString;
        private readonly EntityFactory _entityFactory;

        public SqlServerDataAbstraction(string connString, EntityFactory entityFactory)
        {
            _connString = connString;
            _entityFactory = entityFactory;
        }

        public async Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null) where T : DataEntity
        {
            var many = await BootstrapCommand<IEnumerable<T>>(async (conn, trans) =>
            {
                var result = (await conn.QueryAsync(query, parameters, trans)).Select<dynamic, T>(row =>
                {
                    var constructed = _entityFactory.Make<T>(row);
                    return constructed;
                });

                return result;
            });

            return many;
        }

        public async Task<T> GetSingle<T>(string query, object parameters = null) where T : DataEntity
        {
            var single = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return single;
        }

        public async Task<T> GetSingleOrDefault<T>(string query, object parameters = null) where T : DataEntity
        {
            var singleOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleOrDefaultAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return result;
            });

            return singleOrDefault;
        }

        public async Task<T> GetFirst<T>(string query, object parameters = null) where T : DataEntity
        {
            var first = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return first;
        }

        public async Task<T> GetFirstOrDefault<T>(string query, object parameters = null) where T : DataEntity
        {
            var firstOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstOrDefaultAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return firstOrDefault;
        }

        public async Task UpdateItem(string query, object parameters) 
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

        public async Task<int> AddItem(string query, object parameters)
        {
            return await AddRemoveItem(query, parameters);
        }

        private async Task<int> AddRemoveItem(string query, object parameters)
        {
            var item = await BootstrapCommand<int>(async (conn, trans) =>
            {
                query += "\r\n SELECT CAST(SCOPE_IDENTITY() as int)";
                var recordId = (await conn.QueryAsync<int>(query, parameters, trans)).Single();
                return recordId;
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

    }
}
