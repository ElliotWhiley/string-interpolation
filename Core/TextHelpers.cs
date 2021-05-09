using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        // Only captures values enclosed by an odd number of delimiters, otherwise an escaped character is implied
        static readonly Regex InterpolatedStringSubstitutionPattern = new Regex(@"(?<!\[)(\[\[)*\[(?!\[).*?\]", RegexOptions.IgnoreCase);

        public static string Interpolate(string input, Dictionary<string, string> interpolationValues)
        {
            var interpolationPlaceholders = InterpolatedStringSubstitutionPattern.Matches(input);

            foreach (var placeholder in interpolationPlaceholders) {
                var interpolationPlaceholder = placeholder.ToString();
                var numberOfDelimiters = interpolationPlaceholder.Count(x => x == '[');
                var interpolationPlaceholderKey = interpolationPlaceholder.Replace("[", "").Replace("]", "");
                input = input.Replace("[" + interpolationPlaceholderKey + "]", interpolationValues[interpolationPlaceholderKey]); //try get value
            }

            input = input.Replace("[[", "[");
            input = input.Replace("]]", "]");

            return input;
        }
    }
}