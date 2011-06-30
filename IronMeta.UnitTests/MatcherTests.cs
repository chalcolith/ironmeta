//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (C) 2009-2011, The IronMeta Project
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
using IronMeta.Matcher;
using Xunit;

namespace IronMeta.UnitTests
{

    public class MatcherTests
    {

        [Fact]
        public void TestLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.Literal);

            Assert.True(match.Success);
            Assert.Equal(0, match.StartIndex);
            Assert.Equal(1, match.NextIndex);
        }

        [Fact]
        public void TestLiteral2()
        {
            Assert.False(Run("", "Literal").Success);
        }

        [Fact]
        public void TestLiteralFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("b", parser.Literal);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestAndLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ab", parser.AndLiteral);

            Assert.True(match.Success);
            Assert.Equal(0, match.StartIndex);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestAndLiteralFail1()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.AndLiteral);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestAndLiteralFail2()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("b", parser.AndLiteral);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestOrLiteral()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.OrLiteral);
            Assert.True(match.Success);

            parser = new TestParser();
            match = parser.GetMatch("b", parser.OrLiteral);
            Assert.True(match.Success);
        }

        [Fact]
        public void TestOrLiteral2()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ab", parser.OrLiteral);
            Assert.True(match.Success);
            Assert.Equal(1, match.NextIndex);
        }

        [Fact]
        public void TestOrLiteralFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("ca", parser.OrLiteral);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestLiteralString()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("abcd", parser.LiteralString);
            Assert.True(match.Success);
            Assert.Equal(0, match.StartIndex);
            Assert.Equal(3, match.NextIndex);
        }

        [Fact]
        public void TestLiteralStringFail()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("bca", parser.LiteralString);
            Assert.False(match.Success);
        }

        [Fact]
        public void TestAndString()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("abcde", parser.AndString);
            Assert.True(match.Success);
            Assert.Equal(4, match.NextIndex);
        }

        [Fact]
        public void TestAndStringFail1()
        {
            Assert.False(Run("abc", "AndString").Success);
        }

        [Fact]
        public void TestAndStringFail2()
        {
            Assert.False(Run("abcef", "AndString").Success);
        }

        [Fact]
        public void TestOrString1()
        {
            Assert.True(Run("abe", "OrString").Success);
        }

        [Fact]
        public void TestOrString2()
        {
            Assert.True(Run("cda", "OrString").Success);
        }

        [Fact]
        public void TestOrStringFail()
        {
            Assert.False(Run("badc", "OrString").Success);
        }

        [Fact]
        public void TestFail()
        {
            Assert.False(Run("abc", "Fail").Success);
        }

        [Fact]
        public void TestAny()
        {
            var match = Run("abc", "Any");
            Assert.True(match.Success);
            Assert.Equal(1, match.NextIndex);
        }

        [Fact]
        public void TestLook()
        {
            var match = Run("abcd", "Look");
            Assert.True(match.Success);
            Assert.Equal(3, match.NextIndex);
        }

        [Fact]
        public void TestLookFail()
        {
            Assert.False(Run("abbcd", "Look").Success);
        }

        [Fact]
        public void TestNot()
        {
            var match = Run("acbd", "Not");
            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestStar1()
        {
            var match = Run("a", "Star1");
            Assert.True(match.Success);
            Assert.Equal(1, match.NextIndex);
        }

        [Fact]
        public void TestStar2()
        {
            var match = Run("ab", "Star1");
            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestStar3()
        {
            var match = Run("abbbc", "Star1");
            Assert.True(match.Success);
            Assert.Equal(4, match.NextIndex);
        }

        [Fact]
        public void TestStar4()
        {
            Assert.True(Run("ac", "Star2").Success);
        }

        [Fact]
        public void TestStar5()
        {
            Assert.True(Run("abbbc", "Star2").Success);
        }

        [Fact]
        public void TestStar6()
        {
            Assert.False(Run("abb", "Star2").Success);
        }

        [Fact]
        public void TestPlus1()
        {
            Assert.False(Run("a", "Plus1").Success);
        }

        [Fact]
        public void TestPlus2()
        {
            var match = Run("abbc", "Plus1");
            Assert.True(match.Success);
            Assert.Equal(3, match.NextIndex);
        }

        [Fact]
        public void TestPlus3()
        {
            Assert.True(Run("abcd", "Plus2").Success);
        }

        [Fact]
        public void TestPlus4()
        {
            Assert.True(Run("abbcd", "Plus2").Success);
        }

        [Fact]
        public void TestPlus5()
        {
            Assert.False(Run("ac", "Plus2").Success);
        }

        [Fact]
        public void TestQues()
        {
            Assert.True(Run("ac", "Ques").Success);
        }

        [Fact]
        public void TestQues2()
        {
            Assert.True(Run("abc", "Ques").Success);
        }

        [Fact]
        public void TestQues3()
        {
            Assert.False(Run("abbc", "Ques").Success);
        }

        [Fact]
        public void TestCond1()
        {
            Assert.True(Run("abc", "Cond").Success);
        }

        [Fact]
        public void TestCond2()
        {
            Assert.False(Run("ayc", "Cond").Success);
        }

        [Fact]
        public void TestCond3()
        {
            Assert.True(Run("abc", "Cond2").Success);
        }

        [Fact]
        public void TestCond4()
        {
            Assert.False(Run("ayc", "Cond2").Success);
        }

        [Fact]
        public void TestAction1()
        {
            var match = Run("a", "Action");
            Assert.True(match.Success);
            Assert.NotEqual(123, match.Result);
        }

        [Fact]
        public void TestAction2()
        {
            var match = Run("b", "Action");
            Assert.True(match.Success);
            Assert.Equal(123, match.Result);
        }

        [Fact]
        public void TestClass1()
        {
            Assert.True(Run("a", "Class").Success);
            Assert.True(Run("b", "Class").Success);
        }

        [Fact]
        public void TestClass2()
        {
            Assert.True(Run("\x01", "Class2").Success);
            Assert.True(Run("\x02", "Class2").Success);
            Assert.True(Run("\x03", "Class2").Success);
        }

        [Fact]
        public void TestCall1()
        {
            Assert.True(Run("ac", "Call1").Success);
        }

        [Fact]
        public void TestCall2()
        {
            Assert.True(Run("abc", "Call1").Success);
        }

        [Fact]
        public void TestCall3()
        {
            Assert.True(Run("xay", "Call2").Success);
        }

        [Fact]
        public void TestCall4()
        {
            Assert.True(Run("xby", "Call2").Success);
        }

        [Fact]
        public void TestCall5()
        {
            Assert.False(Run("xy", "Call2").Success);
        }

        [Fact]
        public void TestCall6()
        {
            Assert.True(Run("xy", "Call3").Success);
        }

        [Fact]
        public void TestCall7()
        {
            Assert.False(Run("xy", "Call4").Success);
        }

        [Fact]
        public void TestCall8()
        {
            Assert.True(Run("xy", "Call5").Success);
        }

        [Fact]
        public void TestCall9()
        {
            Assert.False(Run("xy", "Call6").Success);
        }

        [Fact]
        public void TestCallFold()
        {
            Assert.True(Run("xy", "Call7").Success);
        }

        [Fact]
        public void TestCallFail()
        {
            Assert.False(Run("a", "CallFail").Success);
        }

        [Fact]
        public void TestCallClass()
        {
            Assert.True(Run("xy", "CallClass").Success);
        }

        [Fact]
        public void TestCallAny()
        {
            Assert.True(Run("xy", "CallAny").Success);
        }

        [Fact]
        public void TestCallAny2()
        {
            Assert.False(Run("xy", "CallAny2").Success);
        }

        [Fact]
        public void TestCallLook()
        {
            Assert.True(Run("xy", "CallLook").Success);
        }

        [Fact]
        public void TestCallNot()
        {
            Assert.True(Run("xy", "CallNot").Success);
        }

        [Fact]
        public void TestCallNot2()
        {
            Assert.False(Run("xy", "CallNot2").Success);
        }

        [Fact]
        public void TestCallOr()
        {
            Assert.True(Run("xy", "CallOr").Success);
        }

        [Fact]
        public void TestCallOr2()
        {
            Assert.True(Run("xy", "CallOr2").Success);
        }

        [Fact]
        public void TestCallOr3()
        {
            Assert.False(Run("xy", "CallOr3").Success);
        }

        [Fact]
        public void TestCallAnd()
        {
            Assert.True(Run("xy", "CallAnd").Success);
        }

        [Fact]
        public void TestCallAnd2()
        {
            Assert.False(Run("xy", "CallAnd2").Success);
        }

        [Fact]
        public void TestCallStar()
        {
            Assert.True(Run("xy", "CallStar").Success);
        }

        [Fact]
        public void TestCallStar2()
        {
            Assert.True(Run("xy", "CallStar2").Success);
        }

        [Fact]
        public void TestCallPlus()
        {
            Assert.True(Run("xy", "CallPlus").Success);
        }

        [Fact]
        public void TestCallPlus2()
        {
            Assert.False(Run("xy", "CallPlus2").Success);
        }

        [Fact]
        public void TestCallQues()
        {
            Assert.True(Run("xy", "CallQues").Success);
        }

        [Fact]
        public void TestCallQues2()
        {
            Assert.True(Run("xy", "CallQues2").Success);
        }

        [Fact]
        public void TestCallCond()
        {
            Assert.True(Run("xy", "CallCond").Success);
        }

        [Fact]
        public void TestCallCond2()
        {
            Assert.False(Run("xy", "CallCond2").Success);
        }

        [Fact]
        public void TestVarInput()
        {
            var match = Run("aa", "VarInput");
            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestVarInput2()
        {
            Assert.False(Run("a", "VarInput").Success);
        }

        [Fact]
        public void TestCallAct()
        {
            var parser = new TestParser();
            var match = parser.GetMatch("a", parser.CallAct);
            Assert.True(match.Success);
            Assert.NotEqual(42, match.Result);
        }

        [Fact]
        public void TestCallAct2()
        {
            Assert.False(Run("a", "CallAct2").Success);
        }

        [Fact]
        public void TestCallVar()
        {
            Assert.True(Run("xy", "CallCallVar").Success);
        }

        [Fact]
        public void TestCallVar2()
        {
            Assert.False(Run("xy", "CallCallVar2").Success);
        }

        [Fact]
        public void TestCallCallRule()
        {
            Assert.True(Run("xy", "CallCallRule").Success);
        }

        [Fact]
        public void TestDirectLR()
        {
            var match = Run("xy", "DirectLR");

            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestIndirectLR()
        {
            var match = Run("xzy", "IndirectLR_A");

            Assert.True(match.Success);
            Assert.Equal(3, match.NextIndex);
        }

        [Fact]
        public void TestChoiceLR()
        {
            var match = Run("xy", "ChoiceLR");
            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);

            match = Run("xz", "ChoiceLR");
            Assert.True(match.Success);
            Assert.Equal(2, match.NextIndex);
        }

        [Fact]
        public void TestPathalogical()
        {
            var match = Run("dcba", "PathalogicalA");

            Assert.True(match.Success);
            Assert.Equal(4, match.NextIndex);
        }

        [Fact]
        public void TestBuildTask()
        {
            var match = Run("testBuildTask9", "TestBuildTasks");
            Assert.True(match.Success);
        }

        ////////////////////////////////////////

        MatchResult<char, int, _TestParser_Item> Run(IEnumerable<char> input, string productionName)
        {
            var parser = new TestParser();
            var production = (Action<Memo<char, int, _TestParser_Item>, int, IEnumerable<_TestParser_Item>>)
                Delegate.CreateDelegate(typeof(Action<Memo<char, int, _TestParser_Item>, int, IEnumerable<_TestParser_Item>>), parser, productionName);

            return parser.GetMatch(input, production);
        }

    } // class MatcherTests

} // namespace UnitTests
