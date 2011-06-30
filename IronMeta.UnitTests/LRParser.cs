//
// IronMeta LRParser Parser; Generated 30/06/2011 8:48:05 PM UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 1591

namespace IronMeta.UnitTests
{

    using _LRParser_Inputs = IEnumerable<char>;
    using _LRParser_Results = IEnumerable<string>;
    using _LRParser_Args = IEnumerable<_LRParser_Item>;
    using _LRParser_Memo = Memo<char, string, _LRParser_Item>;
    using _LRParser_Rule = System.Action<Memo<char, string, _LRParser_Item>, int, IEnumerable<_LRParser_Item>>;
    using _LRParser_Base = IronMeta.Matcher.Matcher<char, string, _LRParser_Item>;

    public partial class LRParser : IronMeta.Matcher.CharMatcher<string, _LRParser_Item>
    {
        public LRParser()
            : base()
        { }

        public void A(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item a = null;
            _LRParser_Item b = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR A
            _LRParser_Item _r4;

            _r4 = _MemoCall(_memo, "A", _index, A, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND a
            a = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR A
            _LRParser_Item _r6;

            _r6 = _MemoCall(_memo, "A", _index, A, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND b
            b = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)a + (string)b + ")"; }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

            // ACT
            var _r7 = _memo.Results.Peek();
            if (_r7 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r7.StartIndex, _r7.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "a"; }, _r7), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Exp(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            _LRParser_Item e = null;
            _LRParser_Item t = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR Exp
            _LRParser_Item _r5;

            _r5 = _MemoCall(_memo, "Exp", _index, Exp, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND e
            e = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL "+"
            _ParseLiteralString(_memo, ref _index, "+");

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Term
            _LRParser_Item _r8;

            _r8 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND t
            t = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _LRParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)e + " + " + (string)t + ")"; }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Term
            _LRParser_Item _r9;

            _r9 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Term(_LRParser_Memo _memo, int _index, _LRParser_Args _args)
        {

            // LITERAL "1"
            _ParseLiteralString(_memo, ref _index, "1");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _LRParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return "1"; }, _r0), true) );
            }

        }

    } // class LRParser


    public class _LRParser_Item : IronMeta.Matcher.MatchItem<char, string, _LRParser_Item>
    {
        public _LRParser_Item() { }
        public _LRParser_Item(char input) : base(input) { }
        public _LRParser_Item(char input, string result) : base(input, result) { }
        public _LRParser_Item(_LRParser_Inputs inputs) : base(inputs) { }
        public _LRParser_Item(_LRParser_Inputs inputs, _LRParser_Results results) : base(inputs, results) { }
        public _LRParser_Item(int start, int next, _LRParser_Inputs inputs, _LRParser_Results results, bool relative) : base(start, next, inputs, results, relative) { }
        public _LRParser_Item(int start, _LRParser_Inputs inputs) : base(start, start, inputs, Enumerable.Empty<string>(), true) { }
        public _LRParser_Item(_LRParser_Rule production) : base(production) { }

        public static implicit operator List<char>(_LRParser_Item item) { return item != null ? item.Inputs.ToList() : new List<char>(); }
        public static implicit operator char(_LRParser_Item item) { return item != null ? item.Inputs.LastOrDefault() : default(char); }
        public static implicit operator List<string>(_LRParser_Item item) { return item != null ? item.Results.ToList() : new List<string>(); }
        public static implicit operator string(_LRParser_Item item) { return item != null ? item.Results.LastOrDefault() : default(string); }
    }

} // namespace IronMeta.UnitTests

