using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    public static class TextHelpers
    {
        // Only captures values enclosed by an odd number of delimiters, otherwise an escape sequence is implied
        static readonly Regex InterpolatedStringPattern = new Regex(@"(\[\[)+|\[([^\[\]])+\]");

        public static string Interpolate(string input, Dictionary<string, string> mappings)
        {
            if (mappings == null) throw new ArgumentException("Mapping must not be null", nameof(mappings));
            if (mappings.Count == 0) throw new ArgumentException("Mappings must not be empty", nameof(mappings));

            var output = InterpolatedStringPattern.Replace(input, match => {
                var key = match.Value
                    .Replace("[", "")
                    .Replace("]", "");

                // Workaround for regex pattern which matches leading brackets e.g. "[["
                if (string.IsNullOrWhiteSpace(key)) return match.Value;
                if (!mappings.ContainsKey(key)) throw new ArgumentException("Mapping not found for key: " + key, nameof(mappings));
                
                return mappings[key];
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