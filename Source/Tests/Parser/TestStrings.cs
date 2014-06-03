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
using System.Text;

#if __MonoCS__
using NUnit.Framework;
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

using IronMeta.Generator;
using IronMeta.Matcher;

namespace IronMeta.Tests.Parser
{

    [TestClass]
    public class TestStrings
    {

        bool TestParse(string str)
        {
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(str, parser.IronMetaFile);
            return result.Success;
        }

        [TestMethod]
        public void TestStringsDoubleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\'""; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsDoubleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = ""\""""; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsSingleSingle()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\''; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsSingleDouble()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { rule = '\""'; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsCharLiteral()
        {
            var grammar = @"ironmeta StrGrammar<char, string> : CharMatcher<string> { DQ = '\""'; rule = DQ (""\\\"""" | ~DQ .)* DQ; }";
            Assert.IsTrue(TestParse(grammar));
        }

        [TestMethod]
        public void TestStringsTwoSlashes()
        {
            var literal = @"'\\'";
            var parser = new IronMeta.Generator.Parser();
            var result = parser.GetMatch(literal, parser.CSharpCodeItem);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TestStringsFromNarwhal()
        {
            var grammar = @"
    ironmeta StrGrammar<char, string> : CharMatcher<string> {

        CharLiteral = SQ ('\\' (['\'' '""' '\\' '0' 'a' 'b' 'f' 'n' 'r' 't' 'v'] | ['u' 'x'] DECDIGIT+) | ~SQ .):ch SQ SP;
        StrLiteral = DQ (""\\\"""" | ~DQ .)*:str DQ;

        DQ = '""';
	    SQ = '\'';

        DECDIGIT = ['0' - '9'];

    }
";
            Assert.IsTrue(TestParse(grammar));
        }

    }

}
