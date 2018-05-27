using Netify.Common.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Netify.SqlServer
{
    public static class QueryParameterBuilder
    {
        public static (string, string, object) BuildUpdate(IEnumerable<UpdateQueryParameter> queryParameters)
        {
            // i.e.
            // UPDATE Table SET MyColumn = @MyColumn, Other = @Other WHERE MyIdentity = @MyIdentity

            var sets = new List<string>();
            var identity = new List<string>();

            var parameters = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var item in queryParameters)
            {
                if (item.Type == UpdateQueryParameterType.Value)
                {
                    sets.Add($"{item.Key} = @{item.Key}");
                }
                else if (item.Type == UpdateQueryParameterType.Identity)
                {
                    identity.Add($"{item.Key} = @{item.Key}");
                }

                parameters.Add(item.Key, item.Value);
            }

            var setString = string.Join(", ", sets);
            var identityString = $"{string.Join(" AND ", identity)}";

            return (setString, identityString, parameters);
        }

        public static (string, object) BuildGet(IEnumerable<UpdateQueryParameter> queryParameters)
        {
            // i.e.
            // SELECT TOP 1 * FROM Table WHERE Id = @Id
            // SELECT * FROM Table WHERE UserName = @UserName AND Email = @Email

            var identity = new List<string>();
            var parameters = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var param in queryParameters)
            {
                if (param.Type == UpdateQueryParameterType.Identity)
                {
                    identity.Add($"{param.Key} = @{param.Key}");
                    parameters.Add(param.Key, param.Value);
                }
            }

            var identityString = string.Join(" AND ", identity);

            return (identityString, parameters);
        }

        public static (string, string, object) BuildInsert(IEnumerable<InsertQueryParameter> queryParameters)
        {
            // i.e.
            // INSERT INTO Table (Column1, Column2, Column3) VALUES (@Column1, @Column2, @Column3)

            var columns = new List<string>();
            var values = new List<object>();
            var parameters = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var param in queryParameters)
            {
                columns.Add(param.ColumnName);
                values.Add($"@{param.ColumnName}");
                parameters.Add(param.ColumnName, param.Value);
            }

            var columnString = $"({string.Join(", ", columns)})";
            var valuesString = $"({string.Join(", ", values)})";

            return (columnString, valuesString, parameters);
        }
    }
}
