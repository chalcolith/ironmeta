using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;
using IronMeta.Generator;
using IronMeta.Matcher;

namespace IronMeta.UnitTests
{

    public class StringTests
    {

        bool TestParse(string str)
        {
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(str, parser.IronMetaFile);
            return result.Success;
        }

        [Fact]
        public void Test_DoubleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\'""; }";
            Assert.True(TestParse(grammar));
        }

        [Fact]
        public void Test_DoubleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\""""; }";
            Assert.True(TestParse(grammar));
        }

        [Fact]
        public void Test_SingleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\''; }";
            Assert.True(TestParse(grammar));
        }

        [Fact]
        public void Test_SingleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\""'; }";
            Assert.True(TestParse(grammar));
        }

        [Fact]
        public void Test_CharLiteral()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { DQ = '\""'; rule = DQ (""\\\"""" | ~DQ .)* DQ; }";
            Assert.True(TestParse(grammar));
        }

    }

}
