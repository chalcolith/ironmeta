//////////////////////////////////////////////////////////////////////
//
// Copyright © 2013 Verophyle Informatics
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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.LeftRecursion
{

    [TestClass]
    public class TestLeftRecursion
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
