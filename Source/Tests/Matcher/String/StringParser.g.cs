//
// IronMeta StringParser Parser; Generated 2013-08-21 05:02:56Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.Tests.Matcher.String
{

    using _StringParser_Inputs = IEnumerable<string>;
    using _StringParser_Results = IEnumerable<int>;
    using _StringParser_Item = IronMeta.Matcher.MatchItem<string, int>;
    using _StringParser_Args = IEnumerable<IronMeta.Matcher.MatchItem<string, int>>;
    using _StringParser_Memo = Memo<string, int>;
    using _StringParser_Rule = System.Action<Memo<string, int>, int, IEnumerable<IronMeta.Matcher.MatchItem<string, int>>>;
    using _StringParser_Base = IronMeta.Matcher.Matcher<string, int>;

    public partial class StringParser : IronMeta.Matcher.Matcher<string, int>
    {
        public StringParser()
            : base()
        { }

        public StringParser(bool handle_left_recursion)
            : base(handle_left_recursion)
        { }

        public void One(_StringParser_Memo _memo, int _index, _StringParser_Args _args)
        {

            // LITERAL "one"
            _ParseLiteralObj(_memo, ref _index, "one");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _StringParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 1; }, _r0), true) );
            }

        }


        public void Two(_StringParser_Memo _memo, int _index, _StringParser_Args _args)
        {

            // LITERAL "two"
            _ParseLiteralObj(_memo, ref _index, "two");

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _StringParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 2; }, _r0), true) );
            }

        }


        public void Pi(_StringParser_Memo _memo, int _index, _StringParser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // LITERAL "three"
            _ParseLiteralObj(_memo, ref _index, "three");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // LITERAL "point"
            _ParseLiteralObj(_memo, ref _index, "point");

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LITERAL "one"
            _ParseLiteralObj(_memo, ref _index, "one");

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // LITERAL "four"
            _ParseLiteralObj(_memo, ref _index, "four");

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL "one"
            _ParseLiteralObj(_memo, ref _index, "one");

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL "five"
            _ParseLiteralObj(_memo, ref _index, "five");

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // LITERAL "nine"
            _ParseLiteralObj(_memo, ref _index, "nine");

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _StringParser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _StringParser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return 314; }, _r0), true) );
            }

        }

    } // class StringParser

} // namespace IronMeta.Tests.Matcher.String

