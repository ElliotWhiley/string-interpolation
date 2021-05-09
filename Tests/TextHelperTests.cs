using Core;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class TextHelperTests
    {
        [Fact]
        public void InterporlateSubstitutionValuesReturnsInterpolatedString()
        {
            Assert.Equal("Hello Jim", TextHelpers.Interpolate("Hello [name]", new Dictionary<string, string> { { "name", "Jim" } }));
        }

        [Fact]
        public void InterporlateEscapeCharactersReturnsInterpolatedString()
        {
            Assert.Equal("Hello Jim [author]", TextHelpers.Interpolate("Hello [name] [[author]]", new Dictionary<string, string> { { "name", "Jim" } }));
        }

        [Fact]
        public void InterporlateHandleNestedDelimiterInterpolatedString()
        {
            Assert.Equal("Hello Jim [Jam]", TextHelpers.Interpolate("Hello [name] [[[author]]]", new Dictionary<string, string> { { "name", "Jim" }, { "author", "Jam" } }));
        }
    }
}
