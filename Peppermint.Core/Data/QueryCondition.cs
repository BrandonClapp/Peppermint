﻿namespace Peppermint.Core.Data
{
    public enum Is
    {
        EqualTo,
        Like,
        In
    }

    public class QueryCondition
    {
        // can possibly delete 2 of these and only use object ctor
        public QueryCondition(string key, Is type, string value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public QueryCondition(string key, Is type, int value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public QueryCondition(string key, Is type, object value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public Is Type { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
