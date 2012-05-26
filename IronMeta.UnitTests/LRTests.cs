using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

namespace IronMeta.UnitTests
{

    public class LRTests
    {

        [Fact]
        public void TestParseTree()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("aaaa", parser.A);
            Assert.True(match.Success);
            var res = match.Result;
        }

        [Fact]
        public void TestAssociation()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("1+1+1+1", parser.Exp);
            Assert.True(match.Success);
            Assert.Equal("(((1 + 1) + 1) + 1)", match.Result);
        }

        [Fact]
        public void TestNonLR()
        {
            var parser = new LRParser(false);
            var match = parser.GetMatch("ab", parser.NonLR);
            Assert.True(match.Success);

            match = parser.GetMatch("ac", parser.NonLR);
            Assert.True(match.Success);

            match = parser.GetMatch("ad", parser.NonLR);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestLR2()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("ababbba", parser.AAA);
            Assert.True(match.Success);
            var res = match.Result;
        }

        [Fact]
        public void TestHexEscape()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("#\\x000", parser.HexEscapeCharacter);
            Assert.True(match.Success);
        }

    }

}
