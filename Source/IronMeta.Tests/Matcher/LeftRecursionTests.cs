// IronMeta Copyright © The IronMeta Developers

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

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class LeftRecursionTests
    {

        [TestMethod]
        public void TestLeftRecursionParseTree()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("aaaa", parser.A);
            Assert.IsTrue(match.Success);
            var res = match.Result;
        }

        [TestMethod]
        public void TestLeftRecursionAssociation()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("1+1+1+1", parser.Exp);
            Assert.IsTrue(match.Success);
            Assert.AreEqual("(((1 + 1) + 1) + 1)", match.Result);
        }

        [TestMethod]
        public void TestLeftRecursionNonLR()
        {
            var parser = new LRParser(false);
            var match = parser.GetMatch("ab", parser.NonLR);
            Assert.IsTrue(match.Success);

            match = parser.GetMatch("ac", parser.NonLR);
            Assert.IsTrue(match.Success);

            match = parser.GetMatch("ad", parser.NonLR);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestLeftRecursionLR2()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("ababbba", parser.AAA);
            Assert.IsTrue(match.Success);
            var res = match.Result;
        }

        [TestMethod]
        public void TestLeftRecursionHexEscape()
        {
            var parser = new LRParser();
            var match = parser.GetMatch("#\\x000", parser.HexEscapeCharacter);
            Assert.IsTrue(match.Success);
        }
    }
}
