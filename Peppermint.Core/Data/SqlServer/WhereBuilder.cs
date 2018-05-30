using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Peppermint.Core.Data.SqlServer
{
    public static class WhereBuilder
    {
        public static (string, object) Build(IEnumerable<QueryCondition> filters)
        {
            var conditions = new List<string>();
            var parameters = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var filter in filters)
            {
                if (filter.Type == ConditionType.Equals)
                {
                    conditions.Add($"{filter.Key} = @{filter.Key}");
                }
                else if (filter.Type == ConditionType.Like)
                {
                    conditions.Add($"{filter.Key} LIKE %@{filter.Key}%");
                }
                else if (filter.Type == ConditionType.In)
                {
                    conditions.Add($"{filter.Key} IN @{filter.Key}");
                }

                parameters.Add(filter.Key, filter.Value);
            }

            var clause = string.Join(" AND ", conditions);
            return (clause, parameters);
        }

        public static IEnumerable<QueryCondition> GetConditions(IEnumerable<UpdateQueryParameter> queryParameters)
        {
            var conditions = new List<QueryCondition>();

            foreach (var param in queryParameters)
            {
                if (param.Type == UpdateQueryParameterType.Identity)
                {
                    var condition = new QueryCondition(param.Key, ConditionType.Equals, param.Value);
                    conditions.Add(condition);
                }
            }

            return conditions;
        }
    }
}
