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
            var m1 = parser.GetMatch("abcd", parser.ABCD);
            Assert.IsTrue(m1.Success);

            var m2 = parser.GetMatch("abzcd", parser.ABCD);
            Assert.IsTrue(m2.Success);

            var m3 = parser.GetMatch("abd", parser.ABCD);
            Assert.IsFalse(m3.Success);

            var m4 = parser.GetMatch("acd", parser.ABCD);
            Assert.IsFalse(m4.Success);
        }
    }
}
