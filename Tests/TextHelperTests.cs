using Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class TextHelperTests
    {
        [Fact]
        public void InterporlateSubstitutionValuesReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name]", new Dictionary<string, string> { { "name", "Jim" } });
            Assert.Equal("Hello Jim", result);
        }

        [Fact]
        public void InterporlateEscapeCharactersReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name] [[author]]", new Dictionary<string, string> { { "name", "Jim" } });
            Assert.Equal("Hello Jim [author]", result);
        }

        [Fact]
        public void InterporlateUnevenEscapeCharactersReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name] [[:", new Dictionary<string, string> { { "name", "Jim" } });
            Assert.Equal("Hello Jim [:", result);
        }

        [Fact]
        public void InterporlateNestedDelimitersReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name] [[[author]]]", new Dictionary<string, string> { { "name", "Jim" }, { "author", "Jam" } });
            Assert.Equal("Hello Jim [Jam]", result);
        }

        [Fact]
        public void InterporlateEmptyStringReturnsEmptyString()
        {
            var result = TextHelpers.Interpolate("", null);
            Assert.Equal("", result);
        }

        [Fact]
        public void InterporlateMorePlaceholdersThanSubstitutionValuesThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TextHelpers.Interpolate("Hello [name] [author]", new Dictionary<string, string> { { "name", "Jim" } }));
        }

        [Fact]
        public void InterporlateEmptySubstitutionValuesThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TextHelpers.Interpolate("Hello [name] [author]", new Dictionary<string, string>()));
        }

        [Fact]
        public void InterporlateNullSubstitutionValuesThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TextHelpers.Interpolate("Hello [name] [author]", null));
        }
    }
}
