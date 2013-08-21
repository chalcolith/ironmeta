using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IronMeta.Generator;
using IronMeta.Matcher;

namespace IronMeta.Tests.Parser
{

    [TestClass]
    public class TestStrings
    {

        bool TestParse(string str)
        {
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(str, parser.IronMetaFile);
            return result.Success;
        }

        [TestMethod]
        public void TestStringsDoubleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\'""; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsDoubleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\""""; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsSingleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\''; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsSingleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\""'; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsCharLiteral()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { DQ = '\""'; rule = DQ (""\\\"""" | ~DQ .)* DQ; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsTwoSlashes()
        {
            var literal = @"'\\'";
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(literal, parser.CSharpCodeItem);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TestStringsFromNarwhal()
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
            Assert.IsTrue(TestParse(grammar));
        }

    }

}
