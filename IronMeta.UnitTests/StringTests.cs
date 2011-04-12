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

        [Fact]
        public void Test_TwoSlashes()
        {
            var literal = @"'\\'";
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(literal, parser.CSharpCodeItem);
            Assert.True(result.Success);
        }

        [Fact]
        public void Test_FromNarwhal()
        {
            var grammar = @"
    ironmeta StrGrammar<char, string> : CharMatcher<string> {

        CharLiteral = SQ ('\\' (['\'' '""' '\\' '0' 'a' 'b' 'f' 'n' 'r' 't' 'v'] | ['u' 'x'] DECDIGIT+) | ~SQ .):ch SQ SP;
        StrLiteral = DQ (""\\\"""" | ~DQ .)*:str DQ;

        DQ = '""';
	    SQ = '\'';

        DECDIGIT = ['0' - '9'];

    }
";
            Assert.True(TestParse(grammar));
        }

    }

}
