using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.UnitTests.Matcher
{
    [TestClass]
    public class TestComments
    {
        [TestMethod]
        public void TestEndCommentAfterRuleName()
        {
            var parser = new CommentTests();
            var m1 = parser.GetMatch("a", parser.s);
            Assert.IsTrue(m1.Success);
            var m2 = parser.GetMatch("b", parser.s);
            Assert.IsTrue(m2.Success);
            var m3 = parser.GetMatch("z", parser.s);
            Assert.IsFalse(m3.Success);
        }

        [TestMethod]
        public void TestInlineCommentAfterRuleName()
        {
            var parser = new CommentTests();
            var m1 = parser.GetMatch("a", parser.s2);
            Assert.IsTrue(m1.Success);
            var m2 = parser.GetMatch("b", parser.s2);
            Assert.IsTrue(m2.Success);
            var m3 = parser.GetMatch("z", parser.s2);
            Assert.IsFalse(m3.Success);
        }

        [TestMethod]
        public void TestInlineCommentInSequence()
        {
            var parser = new CommentTests();
            var m1 = parser.GetMatch("ab", parser.s3);
            Assert.IsTrue(m1.Success);
            var m2 = parser.GetMatch("b", parser.s3);
            Assert.IsFalse(m2.Success);
            var m3 = parser.GetMatch("z", parser.s3);
            Assert.IsFalse(m3.Success);
        }
    }
}
