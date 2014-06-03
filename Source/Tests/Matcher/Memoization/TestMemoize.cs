//////////////////////////////////////////////////////////////////////
//
// Copyright © 2014 Gordon Tisher
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer.
//     * Redistributions in binary form must reproduce the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer in the documentation and/or other materials 
//       provided with the distribution.
//     * Neither the name of the IronMeta Project nor the names of its 
//       contributors may be used to endorse or promote products 
//       derived from this software without specific prior written 
//       permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE  IMPLIED WARRANTIES OF  MERCHANTABILITY AND FITNESS 
// FOR  A  PARTICULAR  PURPOSE  ARE DISCLAIMED. IN  NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT  LIMITED TO, PROCUREMENT  OF SUBSTITUTE  GOODS  OR SERVICES; 
// LOSS OF USE, DATA, OR  PROFITS; OR  BUSINESS  INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF  LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR  TORT (INCLUDING NEGLIGENCE  OR OTHERWISE) ARISING IN 
// ANY WAY OUT  OF THE  USE OF THIS SOFTWARE, EVEN  IF ADVISED  OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

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
            Assert.AreEqual(2, parser.categoryCount);
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
