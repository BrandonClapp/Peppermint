using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Peppermint.Blog.Utilities
{
    public static class Slug
    {
        public static string Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToLower().Replace(" ", "-");
        }

        public static string Reverse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            value = value.Replace("-", " ");
            value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());

            return value;
        }
    }
}
