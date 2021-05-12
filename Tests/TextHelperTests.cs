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
        public void InterporlateDuplicateKeysReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("[[Name]] is [Name]", new Dictionary<string, string> { { "Name", "Jim" } });
            Assert.Equal("[Name] is Jim", result);
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
        public void InterporlateDelimiterReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name]", new Dictionary<string, string> { { "name", "[:" } });
            Assert.Equal("Hello [:", result);
        }

        [Fact]
        public void InterporlateSubstitutionInMiddleOfWordReturnsInterpolatedString()
        {
            var result = TextHelpers.Interpolate("Hello [name]-Jr", new Dictionary<string, string> { { "name", "Jim" } });
            Assert.Equal("Hello Jim-Jr", result);
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

        [Fact]
        public void InterporlateSubstitutionValueDoesNotMatchThrowsException()
        {
            Assert.Throws<ArgumentException>(() => TextHelpers.Interpolate("Hello [name]", new Dictionary<string, string> { { "author", "Jam" } }));
        }
    }
}
