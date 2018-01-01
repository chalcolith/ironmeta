//
// IronMeta BugFixes Parser; Generated 2016-03-30 04:29:01Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;

using IronMeta.Matcher;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.UnitTests
{

    using _BugFixes_Inputs = IEnumerable<char>;
    using _BugFixes_Results = IEnumerable<object>;
    using _BugFixes_Item = IronMeta.Matcher.MatchItem<char, object>;
    using _BugFixes_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, object>>;
    using _BugFixes_Memo = IronMeta.Matcher.MatchState<char, object>;
    using _BugFixes_Rule = System.Action<IronMeta.Matcher.MatchState<char, object>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, object>>>;
    using _BugFixes_Base = IronMeta.Matcher.Matcher<char, object>;

    public partial class BugFixes : Matcher<char, object>
    {
        public BugFixes()
            : base()
        {
            _setTerminals();
        }

        public BugFixes(bool handle_left_recursion)
            : base(handle_left_recursion)
        {
            _setTerminals();
        }

        void _setTerminals()
        {
            this.Terminals = new HashSet<string>()
            {
                "Bug_3490042_Digit",
                "Bug_3490042_HexDigit",
                "Bug_3490042_HexEscapeCharacter",
                "Bug_3490042_HexScalarValue",
            };
        }


        public void Bug_3490042_HexEscapeCharacter(_BugFixes_Memo _memo, int _index, _BugFixes_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            _BugFixes_Item hex = null;

            // AND 1
            int _start_i1 = _index;

            // LITERAL "#\\x"
            _ParseLiteralString(_memo, ref _index, "#\\x");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR Bug_3490042_HexScalarValue
            _BugFixes_Item _r4;

            _r4 = _MemoCall(_memo, "Bug_3490042_HexScalarValue", _index, Bug_3490042_HexScalarValue, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND hex
            hex = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _BugFixes_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
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
                _memo.Results.Push( new _BugFixes_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { Console.WriteLine("inputs {0}", hex.Inputs);
        return hex.Inputs; }, _r0), true) );
            }

        }


        public void Bug_3490042_HexScalarValue(_BugFixes_Memo _memo, int _index, _BugFixes_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // PLUS 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<object>();
        label0:

            // CALLORVAR Bug_3490042_HexDigit
            _BugFixes_Item _r1;

            _r1 = _MemoCall(_memo, "Bug_3490042_HexDigit", _index, Bug_3490042_HexDigit, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // PLUS 0
            var _r0 = _memo.Results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                if (_index > _start_i0)
                    _memo.Results.Push(new _BugFixes_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

        }


        public void Bug_3490042_HexDigit(_BugFixes_Memo _memo, int _index, _BugFixes_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // OR 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // OR 8
            int _start_i8 = _index;

            // OR 9
            int _start_i9 = _index;

            // OR 10
            int _start_i10 = _index;

            // OR 11
            int _start_i11 = _index;

            // CALLORVAR Bug_3490042_Digit
            _BugFixes_Item _r12;

            _r12 = _MemoCall(_memo, "Bug_3490042_Digit", _index, Bug_3490042_Digit, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // LITERAL 'a'
            _ParseLiteralChar(_memo, ref _index, 'a');

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i10; } else goto label10;

            // LITERAL 'A'
            _ParseLiteralChar(_memo, ref _index, 'A');

        label10: // OR
            int _dummy_i10 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i9; } else goto label9;

            // LITERAL 'b'
            _ParseLiteralChar(_memo, ref _index, 'b');

        label9: // OR
            int _dummy_i9 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i8; } else goto label8;

            // LITERAL 'B'
            _ParseLiteralChar(_memo, ref _index, 'B');

        label8: // OR
            int _dummy_i8 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i7; } else goto label7;

            // LITERAL 'c'
            _ParseLiteralChar(_memo, ref _index, 'c');

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // LITERAL 'C'
            _ParseLiteralChar(_memo, ref _index, 'C');

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // LITERAL 'd'
            _ParseLiteralChar(_memo, ref _index, 'd');

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL 'D'
            _ParseLiteralChar(_memo, ref _index, 'D');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // LITERAL 'e'
            _ParseLiteralChar(_memo, ref _index, 'e');

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL 'E'
            _ParseLiteralChar(_memo, ref _index, 'E');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL 'f'
            _ParseLiteralChar(_memo, ref _index, 'f');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL 'F'
            _ParseLiteralChar(_memo, ref _index, 'F');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Bug_3490042_Digit(_BugFixes_Memo _memo, int _index, _BugFixes_Args _args)
        {

            int _arg_index = 0;
            int _arg_input_index = 0;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // OR 5
            int _start_i5 = _index;

            // OR 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // OR 8
            int _start_i8 = _index;

            // LITERAL '0'
            _ParseLiteralChar(_memo, ref _index, '0');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i8; } else goto label8;

            // LITERAL '1'
            _ParseLiteralChar(_memo, ref _index, '1');

        label8: // OR
            int _dummy_i8 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i7; } else goto label7;

            // LITERAL '2'
            _ParseLiteralChar(_memo, ref _index, '2');

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i6; } else goto label6;

            // LITERAL '3'
            _ParseLiteralChar(_memo, ref _index, '3');

        label6: // OR
            int _dummy_i6 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i5; } else goto label5;

            // LITERAL '4'
            _ParseLiteralChar(_memo, ref _index, '4');

        label5: // OR
            int _dummy_i5 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL '5'
            _ParseLiteralChar(_memo, ref _index, '5');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // LITERAL '6'
            _ParseLiteralChar(_memo, ref _index, '6');

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL '7'
            _ParseLiteralChar(_memo, ref _index, '7');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL '8'
            _ParseLiteralChar(_memo, ref _index, '8');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '9'
            _ParseLiteralChar(_memo, ref _index, '9');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


    } // class BugFixes

} // namespace IronMeta.UnitTests

