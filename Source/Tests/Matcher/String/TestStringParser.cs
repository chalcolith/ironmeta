using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.String
{
    [TestClass]
    public class StringParserTests
    {
        static readonly IEnumerable<string> StrList1 = new List<string> { "one" };
        static readonly IEnumerable<string> StrList2 = new List<string> { "two" };
        static readonly IEnumerable<string> StrListPi = new List<string> { "three", "point", "one", "four", "one", "five", "nine" };

        [TestMethod]
        public void TestStringOne()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrList1, matcher.One);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.Result);
        }

        [TestMethod]
        public void TestStringPi()
        {
            var matcher = new StringParser();

            var match = matcher.GetMatch(StrListPi, matcher.Pi);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(314, match.Result);
        }

    }

}
