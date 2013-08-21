using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.Memoization
{
    [TestClass]
    public class TestMemoize
    {

        [TestMethod]
        public void TestMemoizeChar()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("a", parser.Char);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, parser.charCount);
        }

        [TestMethod]
        public void TestMemoizeSequence()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("a b", parser.SequenceEOF);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, parser.charCount);
        }

        [TestMethod]
        public void TestMemoizeCategory()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("[a b]", parser.SequenceEOF);

            Assert.IsTrue(match.Success);
            Assert.IsTrue(match.Result is CategoryNode);
            Assert.AreEqual(2, ((CategoryNode)match.Result).Children.Count());
            Assert.AreEqual('a', ((CharNode)((CategoryNode)match.Result).Children.ElementAt(0)).Value);
            Assert.AreEqual('b', ((CharNode)((CategoryNode)match.Result).Children.ElementAt(1)).Value);
            Assert.AreEqual(1, parser.categoryCount);
            Assert.AreEqual(2, parser.charCount);
        }

        [TestMethod]
        public void TestMemoizeEmbed()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("[[a b] c]", parser.SequenceEOF);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, parser.charCount);
            Assert.AreEqual(3, parser.categoryCount);
        }

        [TestMethod]
        public void TestMemoizeAlternate()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("a | b", parser.AlternateEOF);

            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestMemoizeCombo()
        {
            var parser = new MemoizeParser();
            var match = parser.GetMatch("a | [b [c d]]", parser.AlternateEOF);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(4, parser.charCount);
            Assert.AreEqual(2, parser.categoryCount);
        }
    }

    partial class MemoizeParser
    {
        public int charCount = 0;
        public int categoryCount = 0;
    }

    public class Node
    {
    }

    public class CharNode : Node
    {
        public char Value { get; set; }
    }

    public class CategoryNode : Node
    {
        public IEnumerable<Node> Children { get; set; }
    }    
}
