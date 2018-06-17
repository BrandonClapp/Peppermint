namespace Peppermint.App.Extentions
{
    public static class StringExtentions
    {
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        public static string Slugify(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToLower().Replace(" ", "-");
        }
    }
}
