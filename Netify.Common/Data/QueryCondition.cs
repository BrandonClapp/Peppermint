namespace Netify.Common.Data
{
    public enum ConditionType
    {
        Equals,
        Like
    }

    public class QueryCondition
    {
        public QueryCondition(string key, ConditionType type, string value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public QueryCondition(string key, ConditionType type, int value)
        {
            Type = type;
            Key = key;
            Value = value;
        }

        public ConditionType Type { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
