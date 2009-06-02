//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) 2009, The IronMeta Project
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
using Xunit;


namespace IronMeta.Matcher
{

    public class MatcherTests : IronMeta.Matcher<char, int>
    {

        public MatcherTests()
            : base(c => (int)c, true)
        { }


        private class MatcherTestsItem : MatchItem
        {
            public MatcherTestsItem()
                : base()
            { 
            }

            public static implicit operator char(MatcherTestsItem item) { return item.Inputs.LastOrDefault(); }
            public static implicit operator int(MatcherTestsItem item) { return item.Results.LastOrDefault(); }
        }


        // to avoid rewriting after we refactored matcher

        private bool Apply(IEnumerable<char> input, string rule, out int result, ref int start, out int next)
        {
            MatchResult mr = Match(input, rule);
            result = mr.Success ? mr.Result : -1;
            start = mr.StartIndex;
            next = mr.NextIndex;
            return mr.Success;
        }

        private bool Apply(IEnumerable<char> input, string rule, out IEnumerable<int> result, ref int start, out int next)
        {
            MatchResult mr = Match(input, rule);
            result = mr.Results;
            start = mr.StartIndex;
            next = mr.NextIndex;
            return mr.Success;
        }

        private bool Apply(IEnumerable<char> input, string rule)
        {
            int result;
            int start = 0, next;
            return Apply(input, rule, out result, ref start, out next);
        }

        //////////////////////////////////////////

        #region Empty

        // TestEmpty = <Empty>
        private Combinator _Empty_Body = null;

        protected IEnumerable<MatchItem> Empty(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Empty_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _EMPTY();
                }
                _Empty_Body = _disj_0;
            }

            foreach (var res in _Empty_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestEmpty()
        {
            IEnumerable<int> result;
            int start = 0;
            int next;

            Assert.True(Apply("", "Empty", out result, ref start, out next));
            Assert.True(start == 0 && next == 0);

            start = 0;
            Assert.True(Apply("a", "Empty", out result, ref start, out next));
            Assert.True(start == 0 && next == 0);
        }

        #endregion


        //////////////////////////////////////////

        #region Any

        // TestAny = .
        private Combinator _Any_Body = null;

        protected IEnumerable<MatchItem> Any(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Any_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _ANY();
                }
                _Any_Body = _disj_0;
            }

            foreach (var res in _Any_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestAny()
        {
            IEnumerable<int> result;
            int start = 0, next;

            Assert.True(Apply("abc", "Any", out result, ref start, out next));
            Assert.True(start == 0 && next == 1);

            Assert.False(Apply("", "Any"));
        }

        #endregion

        //////////////////////////////////////////

        #region Literal

        // TestLiteral = 'a'
        private Combinator _Literal_Body = null;

        protected IEnumerable<MatchItem> Literal(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Literal_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _LITERAL('a');
                }
                _Literal_Body = _disj_0;
            }

            foreach (var res in _Literal_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestLiteral()
        {
            Assert.True(Apply("a", "Literal"));
            Assert.False(Apply("", "Literal"));
            Assert.False(Apply("b", "Literal"));
        }


        protected IEnumerable<MatchItem> FoldedLiteral(int _indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var body = _LITERAL("foo");

            foreach (var res in body.Match(_indent + 1, _inputs, _index, _args, _memo))
                yield return res;
        }


        [Fact]
        public void TestFoldedLiteral()
        {
            Assert.True(Apply("foo", "FoldedLiteral"));
            Assert.False(Apply("bar", "FoldedLiteral"));
            Assert.False(Apply("fo", "FoldedLiteral"));
        }


        #endregion


        //////////////////////////////////////////

        #region And

        // AndAB = 'a' 'b'
        private Combinator _AndAB_Body = null;

        protected IEnumerable<MatchItem> AndAB(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_AndAB_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_LITERAL('a'), _LITERAL('b'));
                }
                _AndAB_Body = _disj_0;
            }

            foreach (var res in _AndAB_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestAndAB()
        {
            IEnumerable<int> result;
            int start = 0, next;

            Assert.True(Apply("ab", "AndAB", out result, ref start, out next));
            Assert.True(start == 0 && next == 2);

            Assert.False(Apply("ba", "AndAB"));
            Assert.False(Apply("a", "AndAB"));
            Assert.False(Apply("b", "AndAB"));
        }

        protected IEnumerable<MatchItem> AndBacktrack(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var body = _AND(_LITERAL('a'), _OR(_LITERAL('b'), _LITERAL('c')), _LITERAL('d'));
            foreach (var res in body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestAndBacktrack()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;

                Assert.True(Apply("abd", "AndBacktrack"));
                Assert.True(Apply("acd", "AndBacktrack"));
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region Or

        // OrAB = 'a' | 'b'
        private Combinator _OrAB_Body = null;

        protected IEnumerable<MatchItem> OrAB(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_OrAB_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _OR(_LITERAL('a'), _LITERAL('b'));
                }
                _OrAB_Body = _disj_0;
            }

            foreach (var res in _OrAB_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestOrAB()
        {
            IEnumerable<int> result;
            int start = 0, next;

            Assert.True(Apply("az", "OrAB", out result, ref start, out next));
            Assert.True(start == 0 && next == 1);

            Assert.True(Apply("b", "OrAB"));
            Assert.False(Apply("", "OrAB"));
            Assert.False(Apply("z", "OrAB"));
            Assert.False(Apply("erefef", "OrAB"));
        }

        #endregion


        //////////////////////////////////////////

        #region Star
        
        // StarABC = 'a' 'b'* 'c'
        private Combinator _StarABC_Body = null;

        protected IEnumerable<MatchItem> StarABC(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_StarABC_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_LITERAL('a'), _STAR(_LITERAL('b'))), _LITERAL('c'));
                }
                _StarABC_Body = _disj_0;
            }

            foreach (var res in _StarABC_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestStarABC()
        {
            IEnumerable<int> result;
            int start = 0, next;

            Assert.True(Apply("abc", "StarABC", out result, ref start, out next));
            Assert.True(start == 0 && next == 3);

            Assert.True(Apply("ac", "StarABC"));
            Assert.True(Apply("abbbbbbbc", "StarABC"));
            Assert.False(Apply("", "StarABC"));
            Assert.False(Apply("ab", "StarABC"));
            Assert.False(Apply("bc", "StarABC"));
        }

        // StarBacktrack = 'a' .* 'z'
        private Combinator _StarBacktrack_Body = null;

        protected IEnumerable<MatchItem> StarBacktrack(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_StarBacktrack_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_LITERAL('a'), _STAR(_ANY())), _LITERAL('z'));
                }
                _StarBacktrack_Body = _disj_0;
            }

            foreach (var res in _StarBacktrack_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestStarBacktrack()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;
                Assert.True(Apply("az", "StarBacktrack"));
                Assert.True(Apply("adfdfdfdz", "StarBacktrack"));
                Assert.False(Apply("zzzzz", "StarBacktrack"));
                Assert.False(Apply("aaaaaaa", "StarBacktrack"));

                IEnumerable<int> res = null;
                int start = 0, next;

                Assert.True(Apply("azzz", "StarBacktrack", out res, ref start, out next));
                Assert.True(start == 0 && next == 4); // make sure it's greedy
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region Plus

        // Plus = 'a' .+ 'c'
        private Combinator _Plus_Body = null;

        protected IEnumerable<MatchItem> Plus(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Plus_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_LITERAL('a'), _PLUS(_ANY())), _LITERAL('c'));
                }
                _Plus_Body = _disj_0;
            }

            foreach (var res in _Plus_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestPlus()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;
                Assert.True(Apply("abc", "Plus"));

                IEnumerable<int> res;
                int start = 0, next;

                Assert.True(Apply("accc", "Plus", out res, ref start, out next));
                Assert.True(start == 0 && next == 4);

                Assert.False(Apply("ac", "Plus"));
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region Ques

        // Ques = 'a' .? 'c'
        private Combinator _Ques_Body = null;

        protected IEnumerable<MatchItem> Ques(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Ques_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_LITERAL('a'), _QUES(_ANY())), _LITERAL('c'));
                }
                _Ques_Body = _disj_0;
            }

            foreach (var res in _Ques_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestQues()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;
                Assert.True(Apply("ac", "Ques"));
                Assert.True(Apply("azc", "Ques"));
                Assert.False(Apply("azdc", "Ques"));
                Assert.False(Apply("", "Ques"));
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region Look

        // Look = &(.+ 'd') 'b' 'c'
        private Combinator _Look_Body = null;

        protected IEnumerable<MatchItem> Look(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Look_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_LOOK( _AND(_PLUS(_ANY()), _LITERAL('d'))), _LITERAL('b')), _LITERAL('c'));
                }
                _Look_Body = _disj_0;
            }

            foreach (var res in _Look_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestLook()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;
                Assert.True(Apply("bcd", "Look"));
                Assert.True(Apply("bcad", "Look"));
                Assert.False(Apply("bca", "Look"));
                Assert.False(Apply("dbc", "Look"));
                Assert.False(Apply("", "Look"));
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region Not

        // Not = ~('a' 'b' 'c') 'a' 'b'
        private Combinator _Not_Body = null;

        protected IEnumerable<MatchItem> Not(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            if (_Not_Body == null)
            {
                Combinator _disj_0 = null;
                {
                    _disj_0 = _AND(_AND(_NOT(_AND(_AND(_LITERAL('a'), _LITERAL('b')), _LITERAL('c'))), _LITERAL('a')), _LITERAL('b'));
                }
                _Not_Body = _disj_0;
            }

            foreach (var res in _Not_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestNot()
        {
            Assert.True(Apply("abd", "Not"));
            Assert.False(Apply("abc", "Not"));
            Assert.False(Apply("", "Not"));
        }

        #endregion


        //////////////////////////////////////////

        #region Condition

        // Condition = (.:c?? (c >= '0' && c <= '9'))+

        protected IEnumerable<MatchItem> Condition(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _disj_0 = null;
            {
                MatcherTestsItem c = new MatcherTestsItem();
                _disj_0 = _PLUS(_CONDITION(_VAR(_ANY(), c), (_imi_) => (c >= '0' && c <= '9')));
            }
            var _Condition_Body = _disj_0;

            foreach (var res in _Condition_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestCondition()
        {
            Assert.True(Apply("0", "Condition"));
            Assert.True(Apply("5", "Condition"));
            Assert.True(Apply("9", "Condition"));
            Assert.False(Apply("a9", "Condition"));

            IEnumerable<int> res;
            int start = 0, next;

            Assert.True(Apply("12345c", "Condition", out res, ref start, out next));
            Assert.True(start == 0 && next == 5);

            Assert.False(Apply("c12345", "Condition"));
        }

        #endregion


        //////////////////////////////////////////

        #region Action

        // ActionSingle = (.:c ?? (c >= '0' && c <= '9'))+:digits 
        //   -> { int total = 0; foreach (int n in digits.Result) total = (total * 10) + (n - '0'); return total; }

        protected IEnumerable<MatchItem> ActionSingle(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _disj_0 = null;
            {
                MatcherTestsItem c = new MatcherTestsItem();
                MatcherTestsItem digits = new MatcherTestsItem();

                _disj_0 = _ACTION(_VAR(_PLUS(_CONDITION(_VAR(_ANY(), c), (_imi_) => (c >= '0' && c <= '9'))), digits),
                    (_imi_) => { int total = 0; foreach (int n in digits.Results) total = (total * 10) + (n - '0'); return total; });
            }
            var _ActionSingle_Body = _disj_0;

            foreach (var res in _ActionSingle_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestAction()
        {
            int res;
            int start = 0, next;

            Assert.True(Apply("12345", "ActionSingle", out res, ref start, out next));
            Assert.True(start == 0 && next == 5);
            Assert.True(res == 12345);

            Assert.False(Apply("abc", "ActionSingle"));
            Assert.False(Apply("", "ActionSingle"));
        }

        #endregion


        //////////////////////////////////////////

        #region Reference

        // Ref = .:c c
        protected IEnumerable<MatchItem> Ref(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _disj_0 = null;
            {
                MatcherTestsItem c = new MatcherTestsItem();
                _disj_0 = _AND(_VAR(_ANY(), c), _REF(c));
            }
            var _Ref_Body = _disj_0;

            foreach (var res in _Ref_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        public void TestRef()
        {
            Assert.True(Apply("aa", "Ref"));
            Assert.True(Apply("bb", "Ref"));
            Assert.False(Apply("", "Ref"));
            Assert.False(Apply("ab", "Ref"));
        }

        #endregion


        //////////////////////////////////////////

        #region Call

        // Literal = 'a'
        // CallSimple = Literal Literal

        // Param 'a':c = c
        // CallParam = .:c Param(c):d -> { return d; }

        protected IEnumerable<MatchItem> Param(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _disj_0 = null;
            {
                MatcherTestsItem c = new MatcherTestsItem();
                _disj_0 = _ARGS(_VAR(_LITERAL('a'), c), _args, _REF(c));
            }
            var _Param_Body = _disj_0;

            foreach (var res in _Param_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> CallParam(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            Combinator _disj_0 = null;
            {
                MatcherTestsItem c = new MatcherTestsItem();
                MatcherTestsItem d = new MatcherTestsItem();
                _disj_0 = _ACTION(_AND(_VAR(_ANY(), c), _VAR(_CALL(Param, new List<MatchItem> { c }), d)), (_imi_) => { return d; });
            }
            var _CallParam_Body = _disj_0;

            foreach (var res in _CallParam_Body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestCallParam()
        {
            int res;
            int start = 0, next;

            Assert.True(Apply("aa", "CallParam", out res, ref start, out next));
            Assert.True(start == 0 && next == 2);
            Assert.True(res == (int)'a');

            Assert.False(Apply("bb", "CallParam"));

            Assert.False(Apply("ab", "CallParam"));
            Assert.False(Apply("", "CallParam"));
        }


        // Double :c = c c
        // Triple :c = c c c
        // PassedProduction :p :c = p(c)
        // PassProduction = PassedProduction(Double, 'a') PassedProduction(Triple, 'b')

        protected IEnumerable<MatchItem> Double(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            MatcherTestsItem c = new MatcherTestsItem();
            var _body = _ARGS(_VAR(_ANY(), c), _args, _AND(_REF(c), _REF(c)));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> Triple(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            MatcherTestsItem c = new MatcherTestsItem();
            var _body = _ARGS(_VAR(_ANY(), c), _args, _AND(_AND(_REF(c), _REF(c)), _REF(c)));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> PassedProduction(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            MatcherTestsItem p = new MatcherTestsItem();
            MatcherTestsItem c = new MatcherTestsItem();

            var _body = _ARGS(_AND(_VAR(_ANY(), p), _VAR(_ANY(), c)), _args, _CALL(p, new List<MatchItem> { c }));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> PassProduction(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _AND(_CALL(PassedProduction, new List<MatchItem> { new MatchItem(Double), new MatchItem('a', CONV('a')) }), _CALL(PassedProduction, new List<MatchItem> { new MatchItem(Triple), new MatchItem('b', CONV('b')) }));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestPassProduction()
        {
            Production p = PassProduction;

            Assert.True(Apply("aabbb", "PassProduction"));
            Assert.False(Apply("", "PassProduction"));
            Assert.False(Apply("aaabbb", "PassProduction"));
        }

        #endregion


        //////////////////////////////////////////

        #region SimpleMemo

        // SubMemoA = 'a' 
        // SubMemo = SubMemoA 'b' | SubMemoA 'c'
        // SimpleMemo = SubMemo 'd' | SubMemo 'z'

        protected IEnumerable<MatchItem> SubMemoA(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _LITERAL('a');

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> SubMemo(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _OR(_AND(_CALL(SubMemoA), _LITERAL('b')), _AND(_CALL(SubMemoA), _LITERAL('c')));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> SimpleMemo(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _OR(_AND(_CALL(SubMemo), _LITERAL('d')), _AND(_CALL(SubMemo), _LITERAL('z')));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestSimpleMemo()
        {
            Assert.True(Apply("acz", "SimpleMemo"));
            Assert.False(Apply("abcz", "SimpleMemo"));
        }

        // StarMemoSub = .*
        // StarMemo = (StarMemoSub 'a' | StarMemoSub 'b') ~.

        protected IEnumerable<MatchItem> StarMemoSub(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _STAR(_ANY());

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> StarMemo(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _AND(_OR(_AND(_CALL(StarMemoSub), _LITERAL('a')), _AND(_CALL(StarMemoSub), _LITERAL('b'))), _NOT(_ANY()));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestStarMemo()
        {
            bool oldStrict = StrictPEG;

            try
            {
                StrictPEG = false;

                int result;
                int start = 0, next;

                Assert.True(Apply("bbbbb", "StarMemo", out result, ref start, out next));
                Assert.True(start == 0 && next == 5);

                Assert.True(Apply("aaaaa", "StarMemo"));
                Assert.False(Apply("aaaaz", "StarMemo"));
                Assert.False(Apply("bbbbz", "StarMemo"));
                Assert.False(Apply("", "StarMemo"));
            }
            finally
            {
                StrictPEG = oldStrict;
            }
        }

        #endregion


        //////////////////////////////////////////

        #region DirectLR

        // DirectLR = DirectLR 'b' | 'a'

        protected IEnumerable<MatchItem> DirectLR(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _OR(_AND(_CALL(DirectLR), _LITERAL('b')), _LITERAL('a'));

            foreach (var m in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return m;
        }

        [Fact]
        public void TestDirectLR()
        {
            int res;
            int start = 0, next;

            Assert.True(Apply("abb", "DirectLR", out res, ref start, out next));
            Assert.True(next == 3);

            Assert.True(Apply("a", "DirectLR"));
            Assert.True(Apply("abbbbbb", "DirectLR"));
            Assert.False(Apply("b", "DirectLR"));
        }

        #endregion


        //////////////////////////////////////////

        #region IndirectLR

        // Indirect = IndirectLR ~.
        // IndirectLR = IndirectSub
        // IndirectNum = .:c ?? (c >= '0' && c <= '9') -> { return c - '0'; }
        // IndirectSub = (IndirectLR:a '-' IndirectNum:b) -> { return (int)a-(int)b; } | IndirectNum

        protected IEnumerable<MatchItem> Indirect(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _AND(_CALL(IndirectLR), _NOT(_ANY()));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> IndirectLR(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            var _body = _CALL(IndirectSub);

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> IndirectNum(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            MatcherTestsItem c = new MatcherTestsItem();

            var _body = _ACTION(_CONDITION(_VAR(_ANY(), c), (_imi_) => (c >= '0' && c <= '9')), (_ini_) => { return c - '0'; });

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        protected IEnumerable<MatchItem> IndirectSub(int indent, IEnumerable<MatchItem> _inputs, int _index, IEnumerable<MatchItem> _args, Memo _memo)
        {
            MatcherTestsItem a = new MatcherTestsItem();
            MatcherTestsItem b = new MatcherTestsItem();

            var _body = _OR(_ACTION(_AND(_AND(_VAR(_CALL(IndirectLR),a), _LITERAL('-')), _VAR(_CALL(IndirectNum), b)), (_ini_) => { return (int)a-(int)b; }), _CALL(IndirectNum));

            foreach (var res in _body.Match(indent + 1, _inputs, _index, null, _memo))
                yield return res;
        }

        [Fact]
        public void TestIndirectLR()
        {
            int res;
            int start = 0, next;

            Assert.True(Apply("3-4", "Indirect", out res, ref start, out next));
            Assert.True(start == 0 && next == 3);
            Assert.True(res == -1);
        }

        #endregion


        //////////////////////////////////////////

    } // class MatcherTests

} // namespace IronMeta.Matcher
