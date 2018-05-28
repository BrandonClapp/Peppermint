using System;
using System.Collections.Generic;
using System.Text;

namespace Peppermint.Common.Data
{
    public class InsertQueryParameter
    {
        public InsertQueryParameter(string columnName, object value)
        {
            ColumnName = columnName;
            Value = value;
        }

        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
}
