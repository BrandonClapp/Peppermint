using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.Common.Data
{
    public enum UpdateQueryParameterType
    {
        Identity,
        Value
    }

    public class UpdateQueryParameter
    {
        public UpdateQueryParameter(string key, UpdateQueryParameterType type, string value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public UpdateQueryParameter(string key, UpdateQueryParameterType type, int value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public UpdateQueryParameter(string key, UpdateQueryParameterType type, object value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public UpdateQueryParameterType Type { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
