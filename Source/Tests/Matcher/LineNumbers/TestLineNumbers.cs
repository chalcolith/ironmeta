using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IronMeta.Tests.Matcher.LineNumbers
{
    [TestClass]
    public class TestLineNumbers
    {

        const string SOURCE1 = 
@"
//////////////////
// line 3; comment
// line 4; comment

ironmeta Test<char, int> : IronMeta.Matcher.Matcher<char, int>
{
    Rule = 'a'; 
    8238gb jd uuuuuu34u4u
}

";

        Generator.Parser parser;

        [TestInitialize]
        public void TestInitialize()
        {
            parser = new Generator.Parser();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            parser = null;
        }

        [TestMethod]
        public void TestLineBeginPositions()
        {
            var match = parser.GetMatch(SOURCE1, parser.IronMetaFile);
            var lineBegins = match.Memo.Positions.OrderBy(n => n).ToArray();

            Assert.AreEqual(2, lineBegins[0], "line 2: 2");
            Assert.AreEqual(22, lineBegins[1], "line 3: 22");
            Assert.AreEqual(42, lineBegins[2], "line 4: 42");
            Assert.AreEqual(62, lineBegins[3], "line 5: 62");
            Assert.AreEqual(64, lineBegins[4], "line 6: 64");
            Assert.AreEqual(128, lineBegins[5], "lin 7: 128");
            Assert.AreEqual(131, lineBegins[6], "line 8: 131");
            Assert.AreEqual(149, lineBegins[7], "line 9: 149");
        }

        [TestMethod]
        public void TestErrorLineAndOffset()
        {
            var match = parser.GetMatch(SOURCE1, parser.IronMetaFile);
            Assert.IsFalse(match.Success, "match should fail");

            int num, offset;
            var line = Generator.Parser.GetLine(match.Memo, match.ErrorIndex, out num, out offset);
            Assert.AreEqual(9, num, "wrong line number");
            Assert.AreEqual(4, offset, "wrong offset");

            Assert.AreEqual("    8238gb jd uuuuuu34u4u", line);
        }

        const string SOURCE2 = @"//////////////////////////////////////////////////////////////////////
// Copyright © 2013 The IronMeta Project
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
// ""AS IS"" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
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

using IronMeta;

    blargh

ironmeta Parser<char, AST.Node> : IronMeta.Matcher.CharMatcher<AST.Node>
{

    Rule = 'a';

}";

        [TestMethod]
        public void TestLongCommentBlock()
        {
            var match = parser.GetMatch(SOURCE2, parser.IronMetaFile);
            Assert.IsFalse(match.Success, "match should fail");

            int num, offset;
            var line = Generator.Parser.GetLine(match.Memo, match.ErrorIndex, out num, out offset);

            Assert.AreEqual(38, num, "line number should be 38");
            Assert.AreEqual(4, offset, "offset should be 4");
        }

    }
}
