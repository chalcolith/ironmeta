using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class RegexpTests
    {
        [TestMethod]
        public void TestSimpleRegexp()
        {
            var parser = new RegexpTest();
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
    }
}
