using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        public static string Interpolate(string input, Dictionary<string, string> interpolationValues)
        {
            var interpolationPlaceholders = Regex.Matches(input, @"\[(.*?)\]");
            // check number of matches == size of dictionary

            foreach (var placeholder in interpolationPlaceholders) {
                var interpolationPlaceholder = placeholder.ToString();
                var interpolationPlaceholderKey = interpolationPlaceholder.Substring(1, interpolationPlaceholder.Length - 2);
                input = input.Replace(interpolationPlaceholder, interpolationValues[interpolationPlaceholderKey]); //try get value
            }

            input = input.Replace("[[", "[");
            input = input.Replace("]]", "]");

            return input;
        }
    }
}