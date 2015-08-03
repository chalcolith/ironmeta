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
    }
}
