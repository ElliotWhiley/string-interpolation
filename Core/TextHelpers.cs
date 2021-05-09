using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        // Only captures values enclosed by an odd number of delimiters, otherwise an escaped character is implied
        static readonly Regex InterpolatedStringSubstitutionPattern = new Regex(@"(?<!\[)(\[\[)*\[(?!\[).*?\]", RegexOptions.IgnoreCase);

        public static string Interpolate(string input, Dictionary<string, string> substitutionValues)
        {
            if (substitutionValues == null) throw new ArgumentException("Substitution values must not be null", nameof(substitutionValues));
            if (substitutionValues.Count == 0) throw new ArgumentException("Substitution values must not be empty", nameof(substitutionValues));

            var interpolationPlaceholders = InterpolatedStringSubstitutionPattern.Matches(input);
            if (interpolationPlaceholders.Count > substitutionValues.Count) throw new ArgumentException("More placeholders than substitution values provided.", nameof(input));

            foreach (var placeholder in interpolationPlaceholders) {
                var interpolationPlaceholder = placeholder.ToString();
                var interpolationPlaceholderKey = interpolationPlaceholder.Replace("[", "").Replace("]", "");
                var foundMatchingValue = substitutionValues.TryGetValue(interpolationPlaceholderKey, out var interpolationPlaceholderValue);
                if (!foundMatchingValue) throw new ArgumentException("Substitution value not found for placeholder: " + interpolationPlaceholderKey, nameof(substitutionValues));

                input = input.Replace("[" + interpolationPlaceholderKey + "]", interpolationPlaceholderValue);
            }

            input = input.Replace("[[", "[");
            input = input.Replace("]]", "]");

            return input;
        }
    }
}