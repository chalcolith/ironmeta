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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IronMeta.Matcher;

namespace IronMeta.Tests.Matcher.Basics
{

    [TestClass]
    public class MatcherTests
    {

        [TestMethod]
        public void TestBasicsLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.Literal);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(0, match.StartIndex);
            Assert.AreEqual(1, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsLiteral2()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("", parser.Literal);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsLiteralFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("b", parser.Literal);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsAndLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ab", parser.AndLiteral);

            Assert.IsTrue(match.Success);
            Assert.AreEqual(0, match.StartIndex);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsAndLiteralFail1()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.AndLiteral);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsAndLiteralFail2()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("b", parser.AndLiteral);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsOrLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.OrLiteral);
            Assert.IsTrue(match.Success);

            parser = new TestParser();
            match = parser.GetMatch("b", parser.OrLiteral);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestBasicsOrLiteral2()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ab", parser.OrLiteral);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsOrLiteralFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ca", parser.OrLiteral);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsLiteralString()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("abcd", parser.LiteralString);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(0, match.StartIndex);
            Assert.AreEqual(3, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsLiteralStringFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("bca", parser.LiteralString);
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsAndString()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("abcde", parser.AndString);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(4, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsAndStringFail1()
        {
            Assert.IsFalse(Run("abc", "AndString").Success);
        }

        [TestMethod]
        public void TestBasicsAndStringFail2()
        {
            Assert.IsFalse(Run("abcef", "AndString").Success);
        }

        [TestMethod]
        public void TestBasicsOrString1()
        {
            Assert.IsTrue(Run("abe", "OrString").Success);
        }

        [TestMethod]
        public void TestBasicsOrString2()
        {
            Assert.IsTrue(Run("cda", "OrString").Success);
        }

        [TestMethod]
        public void TestBasicsOrStringFail()
        {
            Assert.IsFalse(Run("badc", "OrString").Success);
        }

        [TestMethod]
        public void TestBasicsFail()
        {
            Assert.IsFalse(Run("abc", "Fail").Success);
        }

        [TestMethod]
        public void TestBasicsAny()
        {
            var match = Run("abc", "Any");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsLook()
        {
            var match = Run("abcd", "Look");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsLookFail()
        {
            Assert.IsFalse(Run("abbcd", "Look").Success);
        }

        [TestMethod]
        public void TestBasicsNot()
        {
            var match = Run("acbd", "Not");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsStar1()
        {
            var match = Run("a", "Star1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsStar2()
        {
            var match = Run("ab", "Star1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsStar3()
        {
            var match = Run("abbbc", "Star1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(4, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsStar4()
        {
            Assert.IsTrue(Run("ac", "Star2").Success);
        }

        [TestMethod]
        public void TestBasicsStar5()
        {
            Assert.IsTrue(Run("abbbc", "Star2").Success);
        }

        [TestMethod]
        public void TestBasicsStar6()
        {
            Assert.IsFalse(Run("abb", "Star2").Success);
        }

        [TestMethod]
        public void TestBasicsPlus1()
        {
            Assert.IsFalse(Run("a", "Plus1").Success);
        }

        [TestMethod]
        public void TestBasicsPlus2()
        {
            var match = Run("abbc", "Plus1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsPlus3()
        {
            Assert.IsTrue(Run("abcd", "Plus2").Success);
        }

        [TestMethod]
        public void TestBasicsPlus4()
        {
            Assert.IsTrue(Run("abbcd", "Plus2").Success);
        }

        [TestMethod]
        public void TestBasicsPlus5()
        {
            Assert.IsFalse(Run("ac", "Plus2").Success);
        }

        [TestMethod]
        public void TestBasicsQues()
        {
            Assert.IsTrue(Run("ac", "Ques").Success);
        }

        [TestMethod]
        public void TestBasicsQues2()
        {
            Assert.IsTrue(Run("abc", "Ques").Success);
        }

        [TestMethod]
        public void TestBasicsQues3()
        {
            Assert.IsFalse(Run("abbc", "Ques").Success);
        }

        [TestMethod]
        public void TestBasicsCond1()
        {
            Assert.IsTrue(Run("abc", "Cond").Success);
        }

        [TestMethod]
        public void TestBasicsCond2()
        {
            Assert.IsFalse(Run("ayc", "Cond").Success);
        }

        [TestMethod]
        public void TestBasicsCond3()
        {
            Assert.IsTrue(Run("abc", "Cond2").Success);
        }

        [TestMethod]
        public void TestBasicsCond4()
        {
            Assert.IsFalse(Run("ayc", "Cond2").Success);
        }

        [TestMethod]
        public void TestBasicsAction1()
        {
            var match = Run("a", "Action");
            Assert.IsTrue(match.Success);
            Assert.AreNotEqual(123, match.Result);
        }

        [TestMethod]
        public void TestBasicsAction2()
        {
            var match = Run("b", "Action");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(123, match.Result);
        }

        [TestMethod]
        public void TestBasicsClass1()
        {
            Assert.IsTrue(Run("a", "Class").Success);
            Assert.IsTrue(Run("b", "Class").Success);
        }

        [TestMethod]
        public void TestBasicsClass2()
        {
            Assert.IsTrue(Run("\x01", "Class2").Success);
            Assert.IsTrue(Run("\x02", "Class2").Success);
            Assert.IsTrue(Run("\x03", "Class2").Success);
        }

        [TestMethod]
        public void TestBasicsCall1()
        {
            Assert.IsTrue(Run("ac", "Call1").Success);
        }

        [TestMethod]
        public void TestBasicsCall2()
        {
            Assert.IsTrue(Run("abc", "Call1").Success);
        }

        [TestMethod]
        public void TestBasicsCall3()
        {
            Assert.IsTrue(Run("xay", "Call2").Success);
        }

        [TestMethod]
        public void TestBasicsCall4()
        {
            Assert.IsTrue(Run("xby", "Call2").Success);
        }

        [TestMethod]
        public void TestBasicsCall5()
        {
            Assert.IsFalse(Run("xy", "Call2").Success);
        }

        [TestMethod]
        public void TestBasicsCall6()
        {
            Assert.IsTrue(Run("xy", "Call3").Success);
        }

        [TestMethod]
        public void TestBasicsCall7()
        {
            Assert.IsFalse(Run("xy", "Call4").Success);
        }

        [TestMethod]
        public void TestBasicsCall8()
        {
            Assert.IsTrue(Run("xy", "Call5").Success);
        }

        [TestMethod]
        public void TestBasicsCall9()
        {
            Assert.IsFalse(Run("xy", "Call6").Success);
        }

        [TestMethod]
        public void TestBasicsCallFold()
        {
            Assert.IsTrue(Run("xy", "Call7").Success);
        }

        [TestMethod]
        public void TestBasicsCallFail()
        {
            Assert.IsFalse(Run("a", "CallFail").Success);
        }

        [TestMethod]
        public void TestBasicsCallClass()
        {
            Assert.IsTrue(Run("xy", "CallClass").Success);
        }

        [TestMethod]
        public void TestBasicsCallAny()
        {
            Assert.IsTrue(Run("xy", "CallAny").Success);
        }

        [TestMethod]
        public void TestBasicsCallAny2()
        {
            Assert.IsFalse(Run("xy", "CallAny2").Success);
        }

        [TestMethod]
        public void TestBasicsCallLook()
        {
            Assert.IsTrue(Run("xy", "CallLook").Success);
        }

        [TestMethod]
        public void TestBasicsCallNot()
        {
            Assert.IsTrue(Run("xy", "CallNot").Success);
        }

        [TestMethod]
        public void TestBasicsCallNot2()
        {
            Assert.IsFalse(Run("xy", "CallNot2").Success);
        }

        [TestMethod]
        public void TestBasicsCallOr()
        {
            Assert.IsTrue(Run("xy", "CallOr").Success);
        }

        [TestMethod]
        public void TestBasicsCallOr2()
        {
            Assert.IsTrue(Run("xy", "CallOr2").Success);
        }

        [TestMethod]
        public void TestBasicsCallOr3()
        {
            Assert.IsFalse(Run("xy", "CallOr3").Success);
        }

        [TestMethod]
        public void TestBasicsCallAnd()
        {
            Assert.IsTrue(Run("xy", "CallAnd").Success);
        }

        [TestMethod]
        public void TestBasicsCallAnd2()
        {
            Assert.IsFalse(Run("xy", "CallAnd2").Success);
        }

        [TestMethod]
        public void TestBasicsCallStar()
        {
            Assert.IsTrue(Run("xy", "CallStar").Success);
        }

        [TestMethod]
        public void TestBasicsCallStar2()
        {
            Assert.IsTrue(Run("xy", "CallStar2").Success);
        }

        [TestMethod]
        public void TestBasicsCallPlus()
        {
            Assert.IsTrue(Run("xy", "CallPlus").Success);
        }

        [TestMethod]
        public void TestBasicsCallPlus2()
        {
            Assert.IsFalse(Run("xy", "CallPlus2").Success);
        }

        [TestMethod]
        public void TestBasicsCallQues()
        {
            Assert.IsTrue(Run("xy", "CallQues").Success);
        }

        [TestMethod]
        public void TestBasicsCallQues2()
        {
            Assert.IsTrue(Run("xy", "CallQues2").Success);
        }

        [TestMethod]
        public void TestBasicsCallCond()
        {
            Assert.IsTrue(Run("xy", "CallCond").Success);
        }

        [TestMethod]
        public void TestBasicsCallCond2()
        {
            Assert.IsFalse(Run("xy", "CallCond2").Success);
        }

        [TestMethod]
        public void TestBasicsVarInput()
        {
            var match = Run("aa", "VarInput");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsVarInput2()
        {
            Assert.IsFalse(Run("a", "VarInput").Success);
        }

        [TestMethod]
        public void TestBasicsCallAct()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.CallAct);
            Assert.IsTrue(match.Success);
            Assert.AreNotEqual(42, match.Result);
        }

        [TestMethod]
        public void TestBasicsCallAct2()
        {
            Assert.IsFalse(Run("a", "CallAct2").Success);
        }

        [TestMethod]
        public void TestBasicsCallVar()
        {
            Assert.IsTrue(Run("xy", "CallCallVar").Success);
        }

        [TestMethod]
        public void TestBasicsCallVar2()
        {
            Assert.IsFalse(Run("xy", "CallCallVar2").Success);
        }

        [TestMethod]
        public void TestBasicsCallCallRule()
        {
            Assert.IsTrue(Run("xy", "CallCallRule").Success);
        }

        [TestMethod]
        public void TestBasicsDirectLR()
        {
            var match = Run("xy", "DirectLR");

            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsIndirectLR()
        {
            var match = Run("xzy", "IndirectLR_A");

            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsChoiceLR()
        {
            var match = Run("xy", "ChoiceLR");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);

            match = Run("xz", "ChoiceLR");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(2, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsPathalogical()
        {
            var match = Run("dcba", "PathalogicalA");

            Assert.IsTrue(match.Success);
            Assert.AreEqual(4, match.NextIndex);
        }

        [TestMethod]
        public void TestBasicsBuildTask()
        {
            var match = Run("testBuildTask9", "TestBuildTasks");
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestBasicsInputs()
        {
            var match = Run("abc", "TestInputs");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(1, match.Result);
        }

        [TestMethod]
        public void TestBasicsMinMax01()
        {
            var match = Run("aaa", "TestMinMax1");
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestBasicsMinMax02()
        {
            var match = Run("aaaa", "TestMinMax1");
            Assert.IsTrue(match.Success);

            match = Run("aaaaa", "TestMinMax1");
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsMinMax03()
        {
            var match = Run("aa", "TestMinMax1");
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsMinMax04()
        {
            var match = Run("", "TestMinMax1");
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsMinMax05()
        {
            var match = Run("b", "TestMinMax2");
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestBasicsMinMax06()
        {
            var match = Run("", "TestMinMax2");
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void TestBasicsSingleNum()
        {
            var match = Run("aaaab", "TestCount");
            Assert.IsTrue(match.Success);

            match = Run("aaab", "TestCount");
            Assert.IsFalse(match.Success);

            match = Run("aaaaab", "TestCount");
            Assert.IsFalse(match.Success);
        }

        [TestMethod]
        public void TestBasicsReturn()
        {
            var match = Run("b", "TestReturn");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(3, match.Results.Count());
            Assert.AreEqual(4, match.Results.ElementAt(0));
            Assert.AreEqual(5, match.Results.ElementAt(1));
            Assert.AreEqual(6, match.Results.ElementAt(2));
        }

        // left over from an old way of doing things

        MatchResult<char, int> Run(IEnumerable<char> input, string productionName)
        {
            var parser = new TestParser();
            var production = (Action<Memo<char, int>, int, IEnumerable<MatchItem<char, int>>>)
                Delegate.CreateDelegate(typeof(Action<Memo<char, int>, int, IEnumerable<MatchItem<char, int>>>), parser, productionName);

            return parser.GetMatch(input, production);
        }

    } // class MatcherTests

} // namespace UnitTests
