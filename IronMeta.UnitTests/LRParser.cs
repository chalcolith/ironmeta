//
// IronMeta LRParser Parser; Generated 13/04/2011 5:11:14 PM UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

namespace IronMeta.UnitTests
{

    using _LRParser_Inputs = IEnumerable<char>;
    using _LRParser_Results = IEnumerable<string>;
    using _LRParser_Args = IEnumerable<_LRParser_Item>;
    using _LRParser_Rule = System.Action<int, IEnumerable<_LRParser_Item>>;
    using _LRParser_Base = IronMeta.Matcher.Matcher<char, string, _LRParser_Item>;


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

    public partial class LRParser : IronMeta.Matcher.CharMatcher<string, _LRParser_Item>
    {
        public LRParser()
            : base()
        { }

        public void A(int _index, _LRParser_Args _args)
        {

            _LRParser_Item a = null;
            _LRParser_Item b = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR A
            _LRParser_Item _r4;

            _r4 = _MemoCall("A", _index, A, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND a
            a = Results.Peek();

            // AND shortcut
            if (Results.Peek() == null) { Results.Push(null); goto label2; }

            // CALLORVAR A
            _LRParser_Item _r6;

            _r6 = _MemoCall("A", _index, A, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND b
            b = Results.Peek();

        label2: // AND
            var _r2_2 = Results.Pop();
            var _r2_1 = Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                Results.Push( new _LRParser_Item(_start_i2, _index, InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = Results.Peek();
            if (_r1 != null)
            {
                Results.Pop();
                Results.Push( new _LRParser_Item(_r1.StartIndex, _r1.NextIndex, InputEnumerable, _Thunk(_IM_Result => { return "(" + (string)a + (string)b + ")"; }, _r1), true) );
            }

            // OR shortcut
            if (Results.Peek() == null) { Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'a'
            _ParseLiteralChar(ref _index, 'a');

            // ACT
            var _r7 = Results.Peek();
            if (_r7 != null)
            {
                Results.Pop();
                Results.Push( new _LRParser_Item(_r7.StartIndex, _r7.NextIndex, InputEnumerable, _Thunk(_IM_Result => { return "a"; }, _r7), true) );
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }

    } // class LRParser

} // namespace IronMeta.UnitTests

