using Netify.Common.Data;
using Netify.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netify.SqlServer.Abstractions
{
    // todo: Make this DataAbstraction<T> then change all return types to T

    public class PostAbstraction : IDataAccessor<PostEntity>
    {
        private SqlServerDataAbstraction _data;
        private readonly string _tableName = "Posts";

        public PostAbstraction(SqlServerDataAbstraction data)
        {
            _data = data;
        }

        public async Task<IEnumerable<PostEntity>> GetAll()
        {
            var posts = await _data.GetMany<PostEntity>($"SELECT * FROM {_tableName}");
            return posts;
        }

        public async Task<PostEntity> GetOne(IEnumerable<QueryCondition> conditions)
        {
            var (identity, parameters) = WhereBuilder.Build(conditions);

            var post = await _data.GetFirstOrDefault<PostEntity>(
                query: $@"SELECT TOP 1 * FROM {_tableName} WHERE {identity}",
                parameters: parameters
            );

            return post;
        }

        public async Task<PostEntity> Create(IEnumerable<InsertQueryParameter> queryParameters)
        {
            var (columns, values, parameters) = QueryParameterBuilder.BuildInsert(queryParameters);

            var addedId = await _data.AddItem(
                query: $@"
                    INSERT INTO {_tableName} {columns} VALUES {values}
                ",
                parameters: parameters
            );

            // Convention: Primary key is always Id. Can make configurable later if needed.
            var conditions = new List<QueryCondition> {
                new QueryCondition("Id", ConditionType.Equals, addedId)
            };

            var added = await GetOne(conditions);

            return added;
        }

        public async Task<PostEntity> Update(IEnumerable<UpdateQueryParameter> queryParameters)
        {
            var (set, identity, parameters) = QueryParameterBuilder.BuildUpdate(queryParameters);

            await _data.UpdateItem(
                query: $@"
                    UPDATE {_tableName} SET {set} WHERE {identity}
                ",
                parameters: parameters);

            var conditions = WhereBuilder.GetConditions(queryParameters);
            var updated = await GetOne(conditions);
            return updated;
        }

        public async Task<int> Delete(IEnumerable<QueryCondition> conditions)
        {
            var (identity, parameters) = WhereBuilder.Build(conditions);

            var deletedId = await _data.DeleteItem(
                query: $@"
                    DELETE FROM {_tableName} WHERE {identity}
                ",
                parameters: parameters
            );

            return deletedId;
        }

        public async Task<IEnumerable<PostEntity>> GetMany(IEnumerable<QueryCondition> filters)
        {
            var (condition, parameters) = WhereBuilder.Build(filters);

            var posts = await _data.GetMany<PostEntity>(
                query: $@"SELECT * FROM {_tableName} WHERE {condition}",
                parameters: parameters
            );

            return posts;
        }
    }
}
