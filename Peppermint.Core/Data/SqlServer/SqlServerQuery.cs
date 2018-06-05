using Dapper;
using Peppermint.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Peppermint.Core.Data
{
    public class SqlServerQuery
    {
        private readonly string _connString;
        private readonly EntityFactory _entityFactory;

        protected string _query;
        protected bool _whereApplied;

        protected Dictionary<string, object> _parameters = new Dictionary<string, Object>();

        public SqlServerQuery(string connString, EntityFactory entityFactory)
        {
            _connString = connString;
            _entityFactory = entityFactory;
        }

        protected void And()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " AND ";
        }

        protected void Or()
        {
            var current = _query.TrimEnd();
            if (!_whereApplied)
                throw new Exception("Where condition not yet applied.");
            else if (current.EndsWith("AND") || current.EndsWith("OR"))
                throw new Exception("Expecting another condition or execution.");

            _query += " OR ";
        }

        protected void StartGroup()
        {
            _query += " ( ";
        }

        protected void EndGroup()
        {
            _query += " ) ";
        }

        protected void Where(string column, Is type, object value)
        {
            if (!_whereApplied)
            {
                _query += " WHERE ";
                _whereApplied = true;
            }

            // for chaining where's...
            // if multiple where are chained, default to adding end before the new condition
            var current = _query.TrimEnd();
            if (_whereApplied && (!current.EndsWith("AND") || !current.EndsWith("OR")))
            {
                _query += " AND ";
            }

            if (type == Is.EqualTo)
            {
                _query += $"{column} = @{column}";
            }
            else if (type == Is.Like)
            {
                _query += $"{column} LIKE %@{column}%";
            }
            else if (type == Is.In)
            {
                _query += $"{column} IN @{column}";
            }

            _parameters.Add(column, value);
        }

        protected async Task<IEnumerable<T>> GetMany<T>(string query, object parameters = null)
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

        protected async Task<T> GetSingle<T>(string query, object parameters = null)
        {
            var single = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return single;
        }

        protected async Task<T> GetSingleOrDefault<T>(string query, object parameters = null)
        {
            var singleOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QuerySingleOrDefaultAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return result;
            });

            return singleOrDefault;
        }

        protected async Task<T> GetFirst<T>(string query, object parameters = null)
        {
            var first = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return first;
        }

        protected async Task<T> GetFirstOrDefault<T>(string query, object parameters = null)
        {
            var firstOrDefault = await BootstrapCommand<T>(async (conn, trans) =>
            {
                var result = await conn.QueryFirstOrDefaultAsync(query, parameters, trans);
                var constructed = _entityFactory.Make<T>(result);
                return constructed;
            });

            return firstOrDefault;
        }

        protected async Task UpdateItem(string query, object parameters)
        {
            await BootstrapCommand<int>(async (conn, trans) =>
            {
                await conn.ExecuteAsync(query, parameters, trans);
                return 0;
            });
        }

        protected async Task<int> DeleteItem(string query, object parameters)
        {
            return await AddRemoveItem(query, parameters);
        }

        protected async Task<int> AddItem(string query, object parameters)
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

        protected async Task<T> BootstrapCommand<T>(Func<SqlConnection, SqlTransaction, Task<T>> command)
        {
            if (string.IsNullOrEmpty(_connString))
                throw new Exception("No query string configured.");

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
