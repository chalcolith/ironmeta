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

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IronMeta
{

    public class Tests
    {

        /// <summary>
        /// Tests a match to see if it matched and consumed all its input.
        /// </summary>
        private bool MatchGreedy(IEnumerable<char> stream, string productionNAme)
        {
            IronMetaMatcher matcher = new IronMetaMatcher();

            IronMetaMatcher.MatchResult result = matcher.Match(stream, productionNAme);

            return result.Success && result.StartIndex == 0 && result.NextIndex == stream.Count();
        }

        [Fact]
        public void Test_EOF()
        {
            Assert.True(MatchGreedy("", "EOF"));

            IronMetaMatcher matcher = new IronMetaMatcher();

            Assert.False(matcher.Match(" ", "EOF").Success);
            Assert.False(MatchGreedy(" ", "EOF"));
        }

        [Fact]
        public void Test_EOL()
        {
            char [] rnc = new char[] { '\r', '\n' };
            string rn = new string(rnc);

            char[] nc = new char[] { '\n' };
            string n = new string(nc);

            Assert.True(MatchGreedy(rnc, "EOL"));
            Assert.True(MatchGreedy("\n", "EOL"));
            Assert.True(MatchGreedy(n, "EOL"));
            Assert.True(MatchGreedy("\r", "EOL"));
            Assert.False(MatchGreedy("", "EOL"));
            Assert.False(MatchGreedy(" ", "EOL"));
            Assert.False(MatchGreedy("foo", "EOL"));
        }

        [Fact]
        public void Test_Whitespace()
        {
            char[] rnc = new char[] { '\r', '\n' };
            string rn = new string(rnc);

            Assert.True(MatchGreedy(rnc, "Whitespace"));
            Assert.True(MatchGreedy(" ", "Whitespace"));
            Assert.True(MatchGreedy("\t", "Whitespace"));
            Assert.False(MatchGreedy("", "Whitespace"));
            Assert.False(MatchGreedy("  ", "Whitespace"));
        }

        [Fact]
        public void Test_Comment()
        {
            Assert.True(MatchGreedy("//", "Comment"));
            Assert.True(MatchGreedy("// ", "Comment"));
            Assert.True(MatchGreedy("// asdf\n", "Comment"));
            Assert.True(MatchGreedy("/* theitheit*/", "Comment"));
            Assert.True(MatchGreedy("/**/", "Comment"));
            Assert.False(MatchGreedy("/* asdf */ */", "Comment"));
            Assert.False(MatchGreedy("", "Comment"));
        }

        [Fact]
        public void Test_Spacing()
        {
            Assert.True(MatchGreedy("", "Spacing"));
            Assert.True(MatchGreedy("      \t\n\r  ", "Spacing"));
            Assert.True(MatchGreedy("   /* asdf */   ", "Spacing"));
            Assert.True(MatchGreedy("\t\t\t// asdf", "Spacing"));
            Assert.False(MatchGreedy("asdf", "Spacing"));
        }

        [Fact]
        public void Test_Identifier()
        {
            Assert.True(MatchGreedy("foo", "Identifier"));
            Assert.True(MatchGreedy("_foo123", "Identifier"));
            Assert.False(MatchGreedy("123foo", "Identifier"));
            Assert.False(MatchGreedy("", "Identifier"));
        }

        [Fact]
        public void Test_QualifiedIdentifier()
        {
            Assert.True(MatchGreedy("one.two.three", "QualifiedIdentifier"));
            Assert.True(MatchGreedy("one", "QualifiedIdentifier"));
            Assert.False(MatchGreedy("", "QualifiedIdentifier"));

            IronMetaMatcher matcher = new IronMetaMatcher();

            IronMetaMatcher.MatchResult match = matcher.Match("foo.bar.baz", "QualifiedIdentifier");
            Assert.True(match.Success);
            Assert.True(match.Result is IdentifierNode);

            IdentifierNode idn = (IdentifierNode)match.Result;
            Assert.True(idn.Name == "baz");
            Assert.True(idn.Qualifiers.Count == 2);
            Assert.True(idn.Qualifiers[0] == "foo");
            Assert.True(idn.Qualifiers[1] == "bar");
        }

        [Fact]
        public void Test_GenericIdentifier()
        {
            Assert.True(MatchGreedy("foo.bar<one.two, three.four<five>>", "GenericIdentifier"));
            Assert.True(MatchGreedy("foo", "GenericIdentifier"));
            Assert.True(MatchGreedy("foo.bar", "GenericIdentifier"));
            Assert.False(MatchGreedy("foo.bar<>", "GenericIdentifier"));
            Assert.False(MatchGreedy("", "GenericIdentifier"));
        }

        [Fact]
        public void Test_Literal()
        {
            Assert.True(MatchGreedy("{ blah blah { theth ( htiehti ) } }", "Literal"));
            Assert.True(MatchGreedy("( {hth} )", "Literal"));
            Assert.True(MatchGreedy("\" theithiehtien \\\" htiehtieh\"", "Literal"));
            Assert.True(MatchGreedy("' theihtwn \" thieht\\' t'", "Literal"));

            Assert.False(MatchGreedy("", "Literal"));
            Assert.False(MatchGreedy("'thiehtieht", "Literal"));
            Assert.False(MatchGreedy("{ thiehtei", "Literal"));
        }

        [Fact]
        public void Test_RuleCall()
        {
            Assert.True(MatchGreedy("Rule(P1, 'p2')", "RuleCall"));
            Assert.True(MatchGreedy("One.Two.Three()", "RuleCall"));
            Assert.False(MatchGreedy("One.Two", "RuleCall"));
            Assert.False(MatchGreedy("", "RuleCall"));
        }

        [Fact]
        public void Test_ParenTerm()
        {
            Assert.True(MatchGreedy("(one)", "ParenTerm"));
            Assert.True(MatchGreedy("(one | two) ", "ParenTerm"));
            Assert.False(MatchGreedy("one", "ParenTerm"));
            Assert.False(MatchGreedy("", "ParenTerm"));
        }

        [Fact]
        public void Test_AnyTerm()
        {
            Assert.True(MatchGreedy(".", "AnyTerm"));
            Assert.False(MatchGreedy("a", "AnyTerm"));
            Assert.False(MatchGreedy("", "AnyTerm"));
        }

        [Fact]
        public void Test_QuestionTerm()
        {
            Assert.True(MatchGreedy("and?", "QuestionTerm"));
            Assert.False(MatchGreedy("and", "QuestionTerm"));
            Assert.False(MatchGreedy("and??", "QuestionTerm"));
            Assert.False(MatchGreedy("", "QuestionTerm"));
        }

        [Fact]
        public void Test_PlusTerm()
        {
            Assert.True(MatchGreedy("foo+", "PlusTerm"));
            Assert.False(MatchGreedy("foo", "PlusTerm"));
            Assert.True(MatchGreedy("foo++", "PlusTerm"));
            Assert.False(MatchGreedy("", "PlusTerm"));
        }

        [Fact]
        public void Test_StarTerm()
        {
            Assert.True(MatchGreedy("foo*", "StarTerm"));
            Assert.False(MatchGreedy("foo", "StarTerm"));
            Assert.True(MatchGreedy("foo**", "StarTerm"));
            Assert.False(MatchGreedy("", "StarTerm"));
        }

        [Fact]
        public void Test_NotTerm()
        {
            Assert.True(MatchGreedy("~foo", "NotTerm"));
            Assert.False(MatchGreedy("foo", "NotTerm"));
            Assert.True(MatchGreedy("~~foo", "NotTerm"));
            Assert.False(MatchGreedy("", "NotTerm"));
        }

        [Fact]
        public void Test_AndTerm()
        {
            Assert.True(MatchGreedy("&foo", "AndTerm"));
            Assert.False(MatchGreedy("foo", "AndTerm"));
            Assert.True(MatchGreedy("&&foo", "AndTerm"));
            Assert.False(MatchGreedy("", "AndTerm"));
        }

        [Fact]
        public void Test_BoundTerm()
        {
            Assert.True(MatchGreedy("a:b", "BoundTerm"));
            Assert.True(MatchGreedy(":b", "BoundTerm"));
            Assert.True(MatchGreedy("a", "BoundTerm"));
            Assert.False(MatchGreedy("", "BoundTerm"));
        }

        [Fact]
        public void Test_ConditionTerm()
        {
            Assert.True(MatchGreedy("a??(true)", "ConditionExpression"));
            Assert.True(MatchGreedy("a? ??(true)", "ConditionExpression"));
            Assert.True(MatchGreedy("a", "ConditionExpression"));
            Assert.False(MatchGreedy("", "ConditionExpression"));
        }

        [Fact]
        public void Test_SequenceTerm()
        {
            Assert.True(MatchGreedy("foo bar baz", "SequenceExpression"));
            Assert.True(MatchGreedy("foo", "SequenceExpression"));
            Assert.False(MatchGreedy("", "SequenceExpression"));
            Assert.False(MatchGreedy("foo | bar", "SequenceExpression"));
        }

        [Fact]
        public void Test_ActionExp()
        {
            Assert.True(MatchGreedy("foo -> { true; }", "ActionExpression"));
            Assert.True(MatchGreedy("foo", "ActionExpression"));
            Assert.False(MatchGreedy("foo ->", "ActionExpression"));
            Assert.False(MatchGreedy("", "ActionExpression"));
        }

        [Fact]
        public void Test_Calc()
        {
            Program program = new Program();
            Assert.True(program.Process(@"..\..\..\Calc\Calc.ironmeta"));
        }

    } // class Tests

} // namespace IronMeta
