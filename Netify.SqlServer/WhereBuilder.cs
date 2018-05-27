using Netify.Common.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Netify.SqlServer
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

                parameters.Add(filter.Key, filter.Value);
            }

            var clause = string.Join(" AND ", conditions);
            return (clause, parameters);
        }
    }
}
