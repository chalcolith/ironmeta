using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.Combine
{
    [TestClass]
    public class CombineTests
    {

        [TestMethod]
        public void TestCombine()
        {
            var matcher = new Combine2();

            var match = matcher.GetMatch("ghi", matcher.Rule1);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, match.Result);

            match = matcher.GetMatch("jkl", matcher.Rule2);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(4, match.Result);

            match = matcher.GetMatch("abc", matcher.Rule5);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.Result);

            match = matcher.GetMatch("def", matcher.Rule6);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.Result);
        }

        [TestMethod]
        public void TestCombineBase()
        {
            var matcher = new Combine1();

            var match = matcher.GetMatch("virtual", matcher.VirtualRule);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(42, match.Result);
        }

        [TestMethod]
        public void TestCombineOverride()
        {
            var matcher = new Combine2();

            var match = matcher.GetMatch("override", matcher.VirtualRule);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(314, match.Result);
        }

    }

}
