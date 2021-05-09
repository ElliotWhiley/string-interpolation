using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        public static string Interpolate(string input, Dictionary<string, string> interpolationValues)
        {
            var interpolationPlaceholders = Regex.Matches(input, @"([\[].*?)\]");
            // check number of matches == size of dictionary

            foreach (var placeholder in interpolationPlaceholders) {
                var interpolationPlaceholder = placeholder.ToString();
                var numberOfDelimiters = interpolationPlaceholder.Count(x => x == '[');
                // An even number of delimiter characters represents an escaped delimiter character
                if (numberOfDelimiters % 2 == 0) continue;

                var interpolationPlaceholderKey = interpolationPlaceholder.Replace("[", "").Replace("]", "");
                input = input.Replace("[" + interpolationPlaceholderKey + "]", interpolationValues[interpolationPlaceholderKey]); //try get value
            }

            input = input.Replace("[[", "[");
            input = input.Replace("]]", "]");

            return input;
        }
    }
}