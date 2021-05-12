using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        // Only captures values enclosed by an odd number of delimiters, otherwise an escape sequence is implied
        static readonly Regex InterpolatedStringPattern = new Regex(@"(\[\[)+|\[([^\[\]])+\]", RegexOptions.IgnoreCase);

        public static string Interpolate(string input, Dictionary<string, string> substitutionValues)
        {
            if (substitutionValues == null) throw new ArgumentException("Substitution values must not be null", nameof(substitutionValues));
            if (substitutionValues.Count == 0) throw new ArgumentException("Substitution values must not be empty", nameof(substitutionValues));

            var output = InterpolatedStringPattern.Replace(input, match => {
                var key = match.Value
                    .Replace("[", "")
                    .Replace("]", "");

                // Workaround for regex pattern which matches leading brackets e.g. "[["
                if (string.IsNullOrWhiteSpace(key)) return match.Value;
                if (!substitutionValues.ContainsKey(key)) throw new ArgumentException("Substitution value not found for placeholder: " + key, nameof(substitutionValues));
                
                return substitutionValues[key];
            });

            return ReplaceEscapeSequences(output);
        }

        static string ReplaceEscapeSequences(string text)
        {
            return text.Replace("[[", "[")
                .Replace("]]", "]");
        }
    }
}