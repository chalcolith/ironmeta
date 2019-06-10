// IronMeta Copyright © Gordon Tisher 2019

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using IronMeta.Generator;
using IronMeta.Matcher;
using System.IO;

namespace IronMeta.UnitTests
{

    [TestClass]
    public class ParserTests
    {

        bool TestParse(string str)
        {
            var parser = new Parser();
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
            var parser = new Parser();
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

        [TestMethod]
        public void TestNoBaseClass()
        {
            var grammar = @"ironmeta TestGrammar<char, int> { One = ""1""; }";

            var parser = new Parser();
            var result = parser.GetMatch(grammar, parser.IronMetaFile);
            Assert.IsTrue(result.Success);

            var gen = new CSharpGen(result.Result, "TestNoBaseClass");
            string src = null;
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                gen.Generate("testNoBaseClass.ironmeta", sw);
                src = Encoding.UTF8.GetString(ms.ToArray());
            }
            Assert.IsTrue(src.Contains("class TestGrammar : IronMeta.Matcher.Matcher<char, int>"));
        }

        [TestMethod]
        public void TestRegexp()
        {
            var grammar = @"ironmeta TestRe<char, int> { One = /1/; }";
            var parser = new Parser();
            var result = parser.GetMatch(grammar, parser.IronMetaFile);
            Assert.IsTrue(result.Success);
        }
    }
}
