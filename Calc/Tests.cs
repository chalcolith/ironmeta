//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) The IronMeta Project 2009
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//////////////////////////////////////////////////////////////////////

using Xunit;

namespace Calc
{

    public class Tests
    {

        CalcMatcher matcher = new CalcMatcher(c => (int)c, true);

        [Fact]
        public void Test_Number()
        {
            var s = "321";
            CalcMatcher.MatchResult res = matcher.Match(s, "Number");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 321);
        }

        [Fact]
        public void Test_Empty()
        {
            Assert.False(matcher.Match("", "Additive").Success);
        }

        [Fact]
        public void Test_Add()
        {
            var s = "2 + 3";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 5);
        }

        [Fact]
        public void Test_Subtract()
        {
            var s = "123 - 20";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 103);
        }

        [Fact]
        public void Test_Multiplicative()
        {
            var s = "12 / 4";
            var res = matcher.Match(s, "Multiplicative");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 3);
        }

        [Fact]
        public void Test_Multiply()
        {
            var s = "3 * 4";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 12);
        }

        [Fact]
        public void Test_Divide()
        {
            var s = "12 / 3";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 4);
        }

    } // class Tests

} // namespace Calc
