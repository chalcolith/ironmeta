using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class RegexpTests
    {
        RegexpTest parser = new RegexpTest();

        [TestMethod]
        public void TestSimpleRegexp()
        {
            var m1a = parser.GetMatch("abcdd ", parser.ABCD);
            Assert.IsTrue(m1a.Success);
            Assert.AreEqual(m1a.MatchState.InputString.Length - 1, m1a.NextIndex);

            var m1 = parser.GetMatch("abcdd", parser.ABCD);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(m1.MatchState.InputString.Length, m1.NextIndex);

            var m2 = parser.GetMatch("abzcdd", parser.ABCD);
            Assert.IsTrue(m2.Success);
            Assert.AreEqual(m2.MatchState.InputString.Length, m2.NextIndex);

            var m3 = parser.GetMatch("abd", parser.ABCD);
            Assert.IsFalse(m3.Success);

            var m4 = parser.GetMatch("acd", parser.ABCD);
            Assert.IsFalse(m4.Success);

            var m5 = parser.GetMatch("a+bcdd", parser.ABCD);
            Assert.IsTrue(m5.Success);
            Assert.AreEqual(m5.MatchState.InputString.Length, m5.NextIndex);

            var m6 = parser.GetMatch("a-bzcdd", parser.ABCD);
            Assert.IsTrue(m6.Success);
            Assert.AreEqual(m6.MatchState.InputString.Length, m6.NextIndex);
        }

        [TestMethod]
        public void TestIdentifierRegexp()
        {
            var m1 = parser.GetMatch("_", parser.Ident);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(m1.MatchState.InputString.Length, m1.NextIndex);

            var m2 = parser.GetMatch("_12", parser.Ident);
            Assert.IsTrue(m2.Success);
            Assert.AreEqual(m2.MatchState.InputString.Length, m2.NextIndex);

            var m3 = parser.GetMatch("abc", parser.Ident);
            Assert.IsTrue(m3.Success);
            Assert.AreEqual(m3.MatchState.InputString.Length, m3.NextIndex);

            var m4 = parser.GetMatch("0_1", parser.Ident);
            Assert.IsFalse(m4.Success);
        }

        [TestMethod]
        public void TestRegexpNegSet_Issue_19()
        {
            CompareRuleAndRegexp("Hello!");
            CompareRuleAndRegexp("Hello!\n");
            CompareRuleAndRegexp("Hello!\nWorld!\n");
        }

        void CompareRuleAndRegexp(string input)
        {
            var m1 = parser.GetMatch(input, parser.Bar);
            var m2 = parser.GetMatch(input, parser.Foo);
            Assert.AreEqual(m1.Success, m2.Success);
            Assert.AreEqual(m1.StartIndex, m2.StartIndex);
            Assert.AreEqual(m1.NextIndex, m2.NextIndex);
            Assert.AreEqual(m1.Result, m2.Result);
        }

        [TestMethod]
        public void TestQuoteInRegexp_Issue_25()
        {
            var m1 = parser.GetMatch("\"foo\"", parser.Quote);
            Assert.IsTrue(m1.Success);
            var m2 = parser.GetMatch("bar", parser.Quote);
            Assert.IsFalse(m2.Success);
        }

        [TestMethod]
        public void TestWhitespaceInRegexp_Issue_26()
        {
            var m1 = parser.GetMatch("\t\r \n", parser.Whitespace);
            Assert.IsTrue(m1.Success);
            var m2 = parser.GetMatch("abcd", parser.Whitespace);
            Assert.IsFalse(m2.Success);
        }

        [TestMethod]
        public void TestUnicodeInRegexp_Issue_26()
        {
            var m1 = parser.GetMatch("α", parser.GreekAlpha);
            Assert.IsTrue(m1.Success);
        }
    }
}
